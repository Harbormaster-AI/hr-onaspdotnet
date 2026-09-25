
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class ScheduleExceptionRepository : IScheduleExceptionRepository
{
    private readonly ApplicationDbContext _db;

    public ScheduleExceptionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ScheduleException?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ScheduleExceptions
            .Include(x => x.WorkSchedule)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ScheduleException>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ScheduleExceptions
            .AsNoTracking()
            .Include(x => x.WorkSchedule)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ScheduleException scheduleException, CancellationToken cancellationToken)
    {
        _db.ScheduleExceptions.Add(scheduleException);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ScheduleException scheduleException, CancellationToken cancellationToken)
    {
        _db.ScheduleExceptions.Update(scheduleException);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ScheduleException scheduleException, CancellationToken cancellationToken)
    {
        _db.ScheduleExceptions.Remove(scheduleException);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
