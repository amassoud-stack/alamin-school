using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tahfeez.Domain.Entities.Badges;
using Tahfeez.Domain.Enums;
using Tahfeez.Domain.Repositories;
using Tahfeez.Infrastracture.Persistence;

namespace Tahfeez.Infrastracture.BackgroundJobs;

/// <summary>
/// Monthly Hangfire job that:
/// 1. Finds all active students.
/// 2. Sums each student's recitation grades for the previous month.
/// 3. Awards a badge based on the thresholds below (or GradeBookSettings if overridden).
///
/// Badge Thresholds (system defaults):
/// ≥ 75  → بطل الحفظ
/// ≥ 65  → رائد الفصل
/// ≥ 59  → متفوق
/// </summary>
public class BadgeCalculationJob
{
    private readonly AppDbContext _context;
    private readonly IRecitationRepository _recitationRepo;
    private readonly ILogger<BadgeCalculationJob> _logger;

    public BadgeCalculationJob(
        AppDbContext context,
        IRecitationRepository recitationRepo,
        ILogger<BadgeCalculationJob> logger)
    {
        _context = context;
        _recitationRepo = recitationRepo;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 2)]
    public async Task ExecuteAsync()
    {
        var month = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1));
        var firstOfMonth = new DateOnly(month.Year, month.Month, 1);

        _logger.LogInformation("Running badge calculation for {Month}", firstOfMonth.ToString("yyyy-MM"));

        // Load all active students in one query
        var students = await _context.Users
            .Where(u => u.Status == UserStatus.Active && u.ClassId != null)
            .Select(u => new { u.Id, u.ClassId })
            .ToListAsync();

        var badgesToAdd = new List<Badge>();

        foreach (var student in students)
        {
            var totalScore = await _recitationRepo.GetMonthlyTotalScoreAsync(student.Id, firstOfMonth);

            // Check if badge already awarded this month
            var alreadyAwarded = await _context.Badges
                .AnyAsync(b => b.StudentId == student.Id && b.Month == firstOfMonth);

            if (alreadyAwarded) continue;

            var badgeType = totalScore switch
            {
                >= 75 => BadgeType.BatalAlHifz,
                >= 65 => BadgeType.RaidAlFasl,
                >= 59 => BadgeType.Mutafawiq,
                _     => (BadgeType?)null
            };

            if (badgeType is null) continue;

            badgesToAdd.Add(new Badge
            {
                StudentId = student.Id,
                Type = badgeType.Value,
                Month = firstOfMonth,
                TotalScore = totalScore
            });
        }

        if (badgesToAdd.Count > 0)
        {
            _context.Badges.AddRange(badgesToAdd);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Awarded {Count} badges for {Month}", badgesToAdd.Count, firstOfMonth.ToString("yyyy-MM"));
        }
    }
}
