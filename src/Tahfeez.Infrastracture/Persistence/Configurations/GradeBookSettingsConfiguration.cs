using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.GradeBook;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class GradeBookSettingsConfiguration : IEntityTypeConfiguration<GradeBookSettings>
{
    public void Configure(EntityTypeBuilder<GradeBookSettings> builder)
    {
        builder.ToTable("GradeBookSettings");
        builder.HasKey(g => g.Id);
        builder.HasQueryFilter(g => !g.IsDeleted);

        // One settings record per teacher
        builder.HasIndex(g => g.TeacherId).IsUnique();

        builder.HasOne(g => g.Teacher)
            .WithMany()
            .HasForeignKey(g => g.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
