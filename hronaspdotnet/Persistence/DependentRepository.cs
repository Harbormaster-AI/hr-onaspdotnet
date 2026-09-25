
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class DependentRepository : IDependentRepository
{
    private readonly ApplicationDbContext _db;

    public DependentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Dependent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Dependents
            .Include(x => x.BenefitEnrollment)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Dependent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Dependents
            .AsNoTracking()
            .Include(x => x.BenefitEnrollment)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Dependent dependent, CancellationToken cancellationToken)
    {
        _db.Dependents.Add(dependent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Dependent dependent, CancellationToken cancellationToken)
    {
        _db.Dependents.Update(dependent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Dependent dependent, CancellationToken cancellationToken)
    {
        _db.Dependents.Remove(dependent);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
