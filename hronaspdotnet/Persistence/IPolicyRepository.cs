using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Policy policy, CancellationToken cancellationToken);
    Task UpdateAsync(Policy policy, CancellationToken cancellationToken);
    Task DeleteAsync(Policy policy, CancellationToken cancellationToken);

    Task AddToAcknowledgementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAcknowledgementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
