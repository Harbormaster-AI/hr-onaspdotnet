using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IPolicyAcknowledgementRepository
{
    Task<PolicyAcknowledgement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PolicyAcknowledgement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken);
    Task UpdateAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken);
    Task DeleteAsync(PolicyAcknowledgement policyAcknowledgement, CancellationToken cancellationToken);
}
