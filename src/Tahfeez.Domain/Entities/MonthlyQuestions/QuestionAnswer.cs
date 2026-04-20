using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.MonthlyQuestions;

public class QuestionAnswer : AuditableEntity
{
    public Guid QuestionId { get; set; }
    public MonthlyQuestion Question { get; set; } = default!;

    public Guid StudentId { get; set; }
    public User Student { get; set; } = default!;

    public string AnswerText { get; set; } = default!;

    /// <summary>Grade 0–10 awarded by teacher after evaluation (null = not yet graded)</summary>
    public int? Grade { get; set; }

    public string? TeacherFeedback { get; set; }
}
