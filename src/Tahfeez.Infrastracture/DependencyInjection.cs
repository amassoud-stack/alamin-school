using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;
using Tahfeez.Infrastracture.Persistence.Interceptors;
using Tahfeez.Infrastracture.Persistence.Seeders;
using Tahfeez.Infrastracture.Repositories.Attendance;
using Tahfeez.Infrastracture.Repositories.Class;
using Tahfeez.Infrastracture.Repositories.Recitation;
using Tahfeez.Infrastracture.Repositories.Salary;
using Tahfeez.Infrastracture.Repositories.Subscription;
using Tahfeez.Infrastracture.Repositories.User;

namespace Tahfeez.Infrastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // HttpContextAccessor (needed by AuditInterceptor)
        services.AddHttpContextAccessor();

        // Audit interceptor (scoped so it can resolve IHttpContextAccessor per-request)
        services.AddScoped<AuditInterceptor>();

        // Identity (must be registered before OpenIddict)
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<Role>()
        .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IRecitationRepository, RecitationRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ISalaryRepository, SalaryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<Tahfeez.Application.Services.IWhatsAppService,
                              Tahfeez.Infrastracture.Services.TwilioWhatsAppService>();

        services.AddScoped<RolesSeeder>();
        services.AddScoped<UsersSeeder>();
        services.AddScoped<OpeniddictSeeder>();
        services.AddSingleton<DatabaseSeeder>();

        return services;
    }
}
