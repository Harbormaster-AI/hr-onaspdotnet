
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PayrollItemRepository : IPayrollItemRepository
{
    private readonly ApplicationDbContext _db;

    public PayrollItemRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PayrollItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PayrollItems
            .Include(x => x.PayrollRun)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PayrollItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PayrollItems
            .AsNoTracking()
            .Include(x => x.PayrollRun)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PayrollItem payrollItem, CancellationToken cancellationToken)
    {
        _db.PayrollItems.Add(payrollItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PayrollItem payrollItem, CancellationToken cancellationToken)
    {
        _db.PayrollItems.Update(payrollItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PayrollItem payrollItem, CancellationToken cancellationToken)
    {
        _db.PayrollItems.Remove(payrollItem);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
