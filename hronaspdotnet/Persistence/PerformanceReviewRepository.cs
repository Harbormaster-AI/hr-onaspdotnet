using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PerformanceReviewRepository : IPerformanceReviewRepository
{
    private readonly ApplicationDbContext _db;

    public PerformanceReviewRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PerformanceReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PerformanceReviews
            .Include(x => x.Employee)
            .Include(x => x.Reviewer)
            .Include(x => x.Cycle)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PerformanceReview>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PerformanceReviews
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Reviewer)
            .Include(x => x.Cycle)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Add(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Update(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Remove(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
