using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.GradeBook;

/// <summary>
/// Per-teacher configurable thresholds for session grades and monthly badge totals.
/// These defaults match the system spec but can be overridden by each teacher.
/// </summary>
public class GradeBookSettings : AuditableEntity
{
    public Guid TeacherId { get; set; }
    public User Teacher { get; set; } = default!;

    // ── Session grade thresholds (grade 1–10) ──────────────────────
    public int ExcellentMin { get; set; } = 9;   // ممتاز
    public int VeryGoodMin { get; set; } = 7;    // جيد جدًا
    public int GoodMin { get; set; } = 6;        // جيد
    public int AcceptableMin { get; set; } = 5;  // مقبول
    // Below AcceptableMin → إعادة المقرر

    // ── Monthly total thresholds for badge award ───────────────────
    public int BatalAlHifzMin { get; set; } = 75;   // بطل الحفظ  (75–80)
    public int RaidAlFaslMin { get; set; } = 65;    // رائد الفصل (65–74)
    public int MutafawiqMin { get; set; } = 59;     // متفوق      (59–64)
}
