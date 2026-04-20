using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tahfeez.Domain.Entities.MonthlyQuestions;

namespace Tahfeez.Infrastracture.Persistence.Configurations;

public class MonthlyQuestionConfiguration : IEntityTypeConfiguration<MonthlyQuestion>
{
    public void Configure(EntityTypeBuilder<MonthlyQuestion> builder)
    {
        builder.ToTable("MonthlyQuestions");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.QuestionText).IsRequired().HasMaxLength(2000);
        builder.HasQueryFilter(q => !q.IsDeleted);

        builder.HasOne(q => q.Teacher)
            .WithMany()
            .HasForeignKey(q => q.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.Class)
            .WithMany()
            .HasForeignKey(q => q.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.ToTable("QuestionAnswers");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.AnswerText).IsRequired().HasMaxLength(4000);
        builder.Property(a => a.TeacherFeedback).HasMaxLength(1000);
        builder.HasQueryFilter(a => !a.IsDeleted);

        // One answer per student per question
        builder.HasIndex(a => new { a.QuestionId, a.StudentId }).IsUnique();

        builder.HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
