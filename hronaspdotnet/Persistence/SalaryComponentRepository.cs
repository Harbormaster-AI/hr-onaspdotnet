
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class SalaryComponentRepository : ISalaryComponentRepository
{
    private readonly ApplicationDbContext _db;

    public SalaryComponentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SalaryComponent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SalaryComponents
            .Include(x => x.CompensationPackage)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SalaryComponent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SalaryComponents
            .AsNoTracking()
            .Include(x => x.CompensationPackage)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken)
    {
        _db.SalaryComponents.Add(salaryComponent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken)
    {
        _db.SalaryComponents.Update(salaryComponent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken)
    {
        _db.SalaryComponents.Remove(salaryComponent);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
