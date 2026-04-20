using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Classes;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.Mode).IsRequired();

        // Soft-delete global filter
        builder.HasQueryFilter(c => !c.IsDeleted);

        // Teacher FK — restrict delete so we don't accidentally cascade
        builder.HasOne(c => c.Teacher)
            .WithMany()
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Assistant)
            .WithMany()
            .HasForeignKey(c => c.AssistantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Supervisor)
            .WithMany()
            .HasForeignKey(c => c.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Students in class (via User.ClassId)
        builder.HasMany(c => c.Students)
            .WithOne(u => u.Class)
            .HasForeignKey(u => u.ClassId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
