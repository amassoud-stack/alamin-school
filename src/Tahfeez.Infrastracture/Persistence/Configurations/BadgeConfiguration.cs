using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Badges;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.ToTable("Badges");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Type).IsRequired();
        builder.HasQueryFilter(b => !b.IsDeleted);

        // One badge type per student per month
        builder.HasIndex(b => new { b.StudentId, b.Month, b.Type }).IsUnique();

        builder.HasOne(b => b.Student)
            .WithMany()
            .HasForeignKey(b => b.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
