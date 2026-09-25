
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class WorkShiftRepository : IWorkShiftRepository
{
    private readonly ApplicationDbContext _db;

    public WorkShiftRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkShift?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkShifts
            .Include(x => x.WorkSchedule)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkShift>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkShifts
            .AsNoTracking()
            .Include(x => x.WorkSchedule)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkShift workShift, CancellationToken cancellationToken)
    {
        _db.WorkShifts.Add(workShift);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkShift workShift, CancellationToken cancellationToken)
    {
        _db.WorkShifts.Update(workShift);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkShift workShift, CancellationToken cancellationToken)
    {
        _db.WorkShifts.Remove(workShift);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
