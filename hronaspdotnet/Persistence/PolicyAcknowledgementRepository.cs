
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PolicyAcknowledgementRepository : IPolicyAcknowledgementRepository
{
    private readonly ApplicationDbContext _db;

    public PolicyAcknowledgementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PolicyAcknowledgement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PolicyAcknowledgements
            .Include(x => x.Policy)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PolicyAcknowledgement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PolicyAcknowledgements
            .AsNoTracking()
            .Include(x => x.Policy)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken)
    {
        _db.PolicyAcknowledgements.Add(policyAcknowledgement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken)
    {
        _db.PolicyAcknowledgements.Update(policyAcknowledgement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken)
    {
        _db.PolicyAcknowledgements.Remove(policyAcknowledgement);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
