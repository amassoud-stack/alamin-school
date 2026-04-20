using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.EducationalContents;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class EducationalContentConfiguration : IEntityTypeConfiguration<EducationalContent>
{
    public void Configure(EntityTypeBuilder<EducationalContent> builder)
    {
        builder.ToTable("EducationalContents");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
        builder.Property(e => e.YoutubeUrl).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.Category).IsRequired();
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasOne(e => e.UploadedBy)
            .WithMany()
            .HasForeignKey(e => e.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
