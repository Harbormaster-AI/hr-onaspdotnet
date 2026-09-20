using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveRequest>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken);
    Task UpdateAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken);
    Task DeleteAsync(LeaveRequest leaveRequest, CancellationToken cancellationToken);
}
