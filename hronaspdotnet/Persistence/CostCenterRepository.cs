using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CostCenterRepository : ICostCenterRepository
{
    private readonly ApplicationDbContext _db;

    public CostCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CostCenters
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CostCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CostCenters
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Add(costCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Update(costCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Remove(costCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
