
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly ApplicationDbContext _db;

    public TimeEntryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TimeEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TimeEntrys
            .Include(x => x.Timesheet)
            .Include(x => x.Employee)
            .Include(x => x.CostCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TimeEntry>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TimeEntrys
            .AsNoTracking()
            .Include(x => x.Timesheet)
            .Include(x => x.Employee)
            .Include(x => x.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TimeEntry timeEntry, CancellationToken cancellationToken)
    {
        _db.TimeEntrys.Add(timeEntry);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TimeEntry timeEntry, CancellationToken cancellationToken)
    {
        _db.TimeEntrys.Update(timeEntry);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TimeEntry timeEntry, CancellationToken cancellationToken)
    {
        _db.TimeEntrys.Remove(timeEntry);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
