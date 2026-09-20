using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Policy policy, CancellationToken cancellationToken);
    Task UpdateAsync(Policy policy, CancellationToken cancellationToken);
    Task DeleteAsync(Policy policy, CancellationToken cancellationToken);
}
