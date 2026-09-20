using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PayrollCalendarRepository : IPayrollCalendarRepository
{
    private readonly ApplicationDbContext _db;

    public PayrollCalendarRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PayrollCalendar?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PayrollCalendars
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PayrollCalendar>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PayrollCalendars
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Add(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Update(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Remove(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
