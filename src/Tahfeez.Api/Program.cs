using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using Tahfeez.Api.Extentions;
using Tahfeez.Application;
using Tahfeez.Application.Features.Auth.Validators.Register;
using Tahfeez.Infrastracture;
using Tahfeez.Infrastracture.BackgroundJobs;
using Tahfeez.Infrastracture.Persistence;
using Tahfeez.Infrastracture.Persistence.Seeders;
using Tahfeez.Api.Middleware;
using Scalar.AspNetCore;
using Hangfire.MySql;

// Bootstrap logger for startup errors (before config/DI is built)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Tahfeez API...");

    var builder = WebApplication.CreateBuilder(args);
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Configure Serilog from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
                     .ReadFrom.Services(services)
                     .Enrich.FromLogContext());

    builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegisterCommandValidator));

    // Register the database
    builder.AddDatabaseConfig(connectionString);

    // Register services
    builder.AddDiContainer();

    // Register Infrastructure (Identity + repos + seeders) — must be before OpenIddict
    builder.Services.AddInfrastructure();

    // Register OpenIddict (after Identity)
    builder.AddOpenIddictConfig();

    // Register Application (MediatR)
    builder.Services.AddApplication();

    // Register Hangfire with MySQL storage
    builder.Services.AddHangfire(config => config
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseStorage(new MySqlStorage(
            connectionString,
            new MySqlStorageOptions
            {
                TablesPrefix = "Hangfire",
                TransactionIsolationLevel = (System.Transactions.IsolationLevel?)System.Data.IsolationLevel.ReadCommitted
            }
        ))
    );

    builder.Services.AddHangfireServer();
    builder.Services.AddScoped<BadgeCalculationJob>();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi("v1");
    builder.Services.AddSwaggerConfig();

    // add Default Authentication Scheme
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    }).AddBearerToken();

    // Global exception handler (RFC 7807 ProblemDetails)
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddControllers().AddNewtonsoftJson();

    var app = builder.Build();

    // Seed the database on startup
    await using (var scope = app.Services.CreateAsyncScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();
    }
    var seeder = app.Services.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tahfeez API v1");
    });

    // Log every HTTP request/response
    app.UseExceptionHandler();
    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseMiddleware<PendingUserMiddleware>();
    app.UseAuthorization();

    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Tahfeez API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithOpenApiRoutePattern("/openapi/v1.json");
    });

    app.MapControllers();

    // Hangfire dashboard (Admin only in production)
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = [new HangfireDashboardAuthFilter(app.Environment)]
    });

    // Schedule the monthly badge calculation job (1st of every month at 02:00 UTC)
    RecurringJob.AddOrUpdate<BadgeCalculationJob>(
        "monthly-badge-calculation",
        job => job.ExecuteAsync(),
        "0 2 1 * *");  // cron: 02:00 on the 1st of each month

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Tahfeez API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
