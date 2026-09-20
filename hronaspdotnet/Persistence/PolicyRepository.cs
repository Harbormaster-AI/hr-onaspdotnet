using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PolicyRepository : IPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public PolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Policys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Policys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Add(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Update(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Remove(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
