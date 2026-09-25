
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class EmploymentContractRepository : IEmploymentContractRepository
{
    private readonly ApplicationDbContext _db;

    public EmploymentContractRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EmploymentContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.EmploymentContracts
            .Include(x => x.Employee)
            .Include(x => x.CompensationPackage)
            .Include(x => x.WorkSchedule)
            .Include(x => x.Location)
            .Include(x => x.PayrollCalendar)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EmploymentContract>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.EmploymentContracts
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.CompensationPackage)
            .Include(x => x.WorkSchedule)
            .Include(x => x.Location)
            .Include(x => x.PayrollCalendar)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EmploymentContract employmentContract, CancellationToken cancellationToken)
    {
        _db.EmploymentContracts.Add(employmentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmploymentContract employmentContract, CancellationToken cancellationToken)
    {
        _db.EmploymentContracts.Update(employmentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmploymentContract employmentContract, CancellationToken cancellationToken)
    {
        _db.EmploymentContracts.Remove(employmentContract);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
