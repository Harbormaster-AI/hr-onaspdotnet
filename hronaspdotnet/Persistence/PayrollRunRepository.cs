using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PayrollRunRepository : IPayrollRunRepository
{
    private readonly ApplicationDbContext _db;

    public PayrollRunRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PayrollRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PayrollRuns
            .Include(x => x.PayrollCalendar)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PayrollRun>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PayrollRuns
            .AsNoTracking()
            .Include(x => x.PayrollCalendar)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PayrollRun payrollRun, CancellationToken cancellationToken)
    {
        _db.PayrollRuns.Add(payrollRun);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PayrollRun payrollRun, CancellationToken cancellationToken)
    {
        _db.PayrollRuns.Update(payrollRun);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PayrollRun payrollRun, CancellationToken cancellationToken)
    {
        _db.PayrollRuns.Remove(payrollRun);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
