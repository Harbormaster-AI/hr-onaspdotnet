using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PerformanceCycleRepository : IPerformanceCycleRepository
{
    private readonly ApplicationDbContext _db;

    public PerformanceCycleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PerformanceCycle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PerformanceCycles
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PerformanceCycle>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PerformanceCycles
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Add(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Update(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Remove(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
