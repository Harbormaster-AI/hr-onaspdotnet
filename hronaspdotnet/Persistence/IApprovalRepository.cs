using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IApprovalRepository
{
    Task<Approval?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Approval>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Approval approval, CancellationToken cancellationToken);
    Task UpdateAsync(Approval approval, CancellationToken cancellationToken);
    Task DeleteAsync(Approval approval, CancellationToken cancellationToken);


}
