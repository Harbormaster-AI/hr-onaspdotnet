using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IDependentRepository
{
    Task<Dependent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Dependent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Dependent dependent, CancellationToken cancellationToken);
    Task UpdateAsync(Dependent dependent, CancellationToken cancellationToken);
    Task DeleteAsync(Dependent dependent, CancellationToken cancellationToken);


}
