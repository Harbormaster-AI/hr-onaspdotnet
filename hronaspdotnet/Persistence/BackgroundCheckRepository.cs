
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class BackgroundCheckRepository : IBackgroundCheckRepository
{
    private readonly ApplicationDbContext _db;

    public BackgroundCheckRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BackgroundCheck?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BackgroundChecks
            .Include(x => x.Candidate)
            .Include(x => x.Requisition)
            .Include(x => x.Report)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BackgroundCheck>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BackgroundChecks
            .AsNoTracking()
            .Include(x => x.Candidate)
            .Include(x => x.Requisition)
            .Include(x => x.Report)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken)
    {
        _db.BackgroundChecks.Add(backgroundCheck);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken)
    {
        _db.BackgroundChecks.Update(backgroundCheck);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken)
    {
        _db.BackgroundChecks.Remove(backgroundCheck);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
