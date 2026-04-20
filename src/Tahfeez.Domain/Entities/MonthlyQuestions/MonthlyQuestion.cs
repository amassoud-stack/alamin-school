using Tahfeez.Domain.Entities.Classes;
using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.MonthlyQuestions;

public class MonthlyQuestion : AuditableEntity
{
    public Guid TeacherId { get; set; }
    public User Teacher { get; set; } = default!;

    public Guid ClassId { get; set; }
    public Class Class { get; set; } = default!;

    public string QuestionText { get; set; } = default!;
    public DateOnly Month { get; set; }

    public bool IsActive { get; set; } = true;
    public ICollection<QuestionAnswer> Answers { get; set; } = [];
}
