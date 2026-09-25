
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class LeavePolicyRepository : ILeavePolicyRepository
{
    private readonly ApplicationDbContext _db;

    public LeavePolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LeavePolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LeavePolicys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LeavePolicy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LeavePolicys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken)
    {
        _db.LeavePolicys.Add(leavePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken)
    {
        _db.LeavePolicys.Update(leavePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken)
    {
        _db.LeavePolicys.Remove(leavePolicy);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToLeaveRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LeaveRequests
            .Where(leaveRequest =>
                request.ChildIds.Contains(leaveRequest.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    leaveRequest =>
                        EF.Property<Guid?>(
                            leaveRequest,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLeaveRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LeaveRequests
            .Where(leaveRequest =>
                request.ChildIds.Contains(leaveRequest.Id) &&
                EF.Property<Guid?>(
                    leaveRequest,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    leaveRequest =>
                        EF.Property<Guid?>(
                            leaveRequest,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
