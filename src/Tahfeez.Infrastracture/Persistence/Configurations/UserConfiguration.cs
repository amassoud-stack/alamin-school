using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Users;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(300);
        builder.Property(u => u.PhoneNumber2).HasMaxLength(20);
        builder.Property(u => u.Level).HasMaxLength(100);
        builder.Property(u => u.Status).IsRequired();

        // Soft-delete global filter
        builder.HasQueryFilter(u => !u.IsDeleted);

        // Student attendances
        builder.HasMany(u => u.Attendances)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Student subscriptions
        builder.HasMany(u => u.Subscriptions)
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
