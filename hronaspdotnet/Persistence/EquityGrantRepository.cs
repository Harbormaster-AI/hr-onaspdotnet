
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class EquityGrantRepository : IEquityGrantRepository
{
    private readonly ApplicationDbContext _db;

    public EquityGrantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EquityGrant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.EquityGrants
            .Include(x => x.CompensationPackage)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EquityGrant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.EquityGrants
            .AsNoTracking()
            .Include(x => x.CompensationPackage)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EquityGrant equityGrant, CancellationToken cancellationToken)
    {
        _db.EquityGrants.Add(equityGrant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EquityGrant equityGrant, CancellationToken cancellationToken)
    {
        _db.EquityGrants.Update(equityGrant);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EquityGrant equityGrant, CancellationToken cancellationToken)
    {
        _db.EquityGrants.Remove(equityGrant);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
