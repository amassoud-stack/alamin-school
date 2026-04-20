using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.Recitations;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class RecitationConfiguration : IEntityTypeConfiguration<Recitation>
{
    public void Configure(EntityTypeBuilder<Recitation> builder)
    {
        builder.ToTable("Recitations");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Grade)
            .IsRequired()
            .HasAnnotation("CheckConstraint", "Grade >= 1 AND Grade <= 10");

        builder.Property(r => r.AyahsCount).IsRequired();
        builder.Property(r => r.Type).IsRequired();
        builder.Property(r => r.Notes).HasMaxLength(1000);

        builder.HasQueryFilter(r => !r.IsDeleted);

        builder.HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Teacher)
            .WithMany()
            .HasForeignKey(r => r.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
