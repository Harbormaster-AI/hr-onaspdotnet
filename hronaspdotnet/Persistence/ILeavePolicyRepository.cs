using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ILeavePolicyRepository
{
    Task<LeavePolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LeavePolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken);
    Task UpdateAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken);
    Task DeleteAsync(LeavePolicy leavePolicy, CancellationToken cancellationToken);

    Task AddToLeaveRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLeaveRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
