using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITerminationRepository
{
    Task<Termination?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Termination>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Termination termination, CancellationToken cancellationToken);
    Task UpdateAsync(Termination termination, CancellationToken cancellationToken);
    Task DeleteAsync(Termination termination, CancellationToken cancellationToken);


}
