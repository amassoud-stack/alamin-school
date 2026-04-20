using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Tahfeez.Domain.Entities.Atendences;
using Tahfeez.Domain.Entities.Badges;
using Tahfeez.Domain.Entities.Classes;
using Tahfeez.Domain.Entities.Competitions;
using Tahfeez.Domain.Entities.EducationalContents;
using Tahfeez.Domain.Entities.GradeBook;
using Tahfeez.Domain.Entities.Messages;
using Tahfeez.Domain.Entities.MonthlyQuestions;
using Tahfeez.Domain.Entities.Payments;
using Tahfeez.Domain.Entities.Recitations;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Entities.Salaries;
using Tahfeez.Domain.Entities.Subscriptions;
using Tahfeez.Domain.Entities.Users;

namespace Tahfeez.Infrastracture.Persistence;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Atendence> Attendances { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Recitation> Recitations { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<CompetitionEntry> CompetitionEntries { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<EducationalContent> EducationalContents { get; set; }
    public DbSet<MonthlyQuestion> MonthlyQuestions { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<GradeBookSettings> GradeBookSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasCharSet(CharSet.Utf8Mb4, DelegationModes.ApplyToColumns);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.UserName).HasMaxLength(191);
            entity.Property(u => u.NormalizedUserName).HasMaxLength(191);
            entity.Property(u => u.Email).HasMaxLength(191);
            entity.Property(u => u.NormalizedEmail).HasMaxLength(191);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(r => r.Name).HasMaxLength(191);
            entity.Property(r => r.NormalizedName).HasMaxLength(191);
        });

        // OpenIddict - fix composite index key length
        modelBuilder.Entity<OpenIddictEntityFrameworkCoreAuthorization>(entity =>
        {
            entity.Property(a => a.Subject).HasMaxLength(200); // was 400
        });

        modelBuilder.Entity<OpenIddictEntityFrameworkCoreToken>(entity =>
        {
            entity.Property(t => t.Subject).HasMaxLength(200); // was 400
        });

        // Apply all IEntityTypeConfiguration<T> from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }
}
