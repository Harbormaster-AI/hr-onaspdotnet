using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly ApplicationDbContext _db;

    public TimesheetRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Timesheet?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Timesheets
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Timesheet>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Timesheets
            .AsNoTracking()
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Timesheet timesheet, CancellationToken cancellationToken)
    {
        _db.Timesheets.Add(timesheet);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Timesheet timesheet, CancellationToken cancellationToken)
    {
        _db.Timesheets.Update(timesheet);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Timesheet timesheet, CancellationToken cancellationToken)
    {
        _db.Timesheets.Remove(timesheet);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
