using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class LeaveRequestRepository : ILeaveRequestRepository
{
    private readonly ApplicationDbContext _db;

    public LeaveRequestRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LeaveRequests
            .Include(x => x.Employee)
            .Include(x => x.LeavePolicy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LeaveRequests
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.LeavePolicy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken)
    {
        _db.LeaveRequests.Add(leaveRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken)
    {
        _db.LeaveRequests.Update(leaveRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken)
    {
        _db.LeaveRequests.Remove(leaveRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
