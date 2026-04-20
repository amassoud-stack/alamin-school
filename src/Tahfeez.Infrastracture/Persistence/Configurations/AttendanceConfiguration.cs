using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Atendences;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Atendence>
{
    public void Configure(EntityTypeBuilder<Atendence> builder)
    {
        builder.ToTable("Attendances");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status).IsRequired();
        builder.Property(a => a.Notes).HasMaxLength(500);

        builder.HasQueryFilter(a => !a.IsDeleted);

        // Composite unique: one record per user per day
        builder.HasIndex(a => new { a.UserId, a.Date }).IsUnique();
    }
}
