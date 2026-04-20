using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Salaries;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("Salaries");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Amount).HasColumnType("numeric(18,2)").IsRequired();
        builder.Property(s => s.Status).IsRequired();
        builder.Property(s => s.Notes).HasMaxLength(500);

        builder.HasQueryFilter(s => !s.IsDeleted);

        // One salary record per user per month
        builder.HasIndex(s => new { s.UserId, s.SalaryMonth }).IsUnique();

        builder.HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
