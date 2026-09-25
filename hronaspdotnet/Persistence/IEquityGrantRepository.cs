using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IEquityGrantRepository
{
    Task<EquityGrant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EquityGrant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(EquityGrant equityGrant, CancellationToken cancellationToken);
    Task UpdateAsync(EquityGrant equityGrant, CancellationToken cancellationToken);
    Task DeleteAsync(EquityGrant equityGrant, CancellationToken cancellationToken);


}
