using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class WorkScheduleRepository : IWorkScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public WorkScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkSchedules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Add(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Update(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Remove(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
