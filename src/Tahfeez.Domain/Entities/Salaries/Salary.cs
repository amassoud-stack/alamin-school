using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Salaries;

public class Salary : AuditableEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    /// <summary>Role at time of salary payment (denormalised for reporting)</summary>
    public UserRole Role { get; set; }

    public decimal Amount { get; set; }
    public SalaryStatus Status { get; set; } = SalaryStatus.Unpaid;

    /// <summary>The month this salary covers (first day of month UTC)</summary>
    public DateOnly SalaryMonth { get; set; }

    public DateTime? PaidAt { get; set; }
    public string? Notes { get; set; }
}
