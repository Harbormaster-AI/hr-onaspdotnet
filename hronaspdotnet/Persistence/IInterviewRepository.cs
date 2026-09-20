using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IInterviewRepository
{
    Task<Interview?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Interview>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Interview interview, CancellationToken cancellationToken);
    Task UpdateAsync(Interview interview, CancellationToken cancellationToken);
    Task DeleteAsync(Interview interview, CancellationToken cancellationToken);
}
