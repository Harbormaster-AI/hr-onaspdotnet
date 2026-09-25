
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class ApprovalRepository : IApprovalRepository
{
    private readonly ApplicationDbContext _db;

    public ApprovalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Approval?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Approvals
            .Include(x => x.Approver)
            .Include(x => x.Timesheet)
            .Include(x => x.LeaveRequest)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Approval>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Approvals
            .AsNoTracking()
            .Include(x => x.Approver)
            .Include(x => x.Timesheet)
            .Include(x => x.LeaveRequest)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Approval approval, CancellationToken cancellationToken)
    {
        _db.Approvals.Add(approval);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Approval approval, CancellationToken cancellationToken)
    {
        _db.Approvals.Update(approval);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Approval approval, CancellationToken cancellationToken)
    {
        _db.Approvals.Remove(approval);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
