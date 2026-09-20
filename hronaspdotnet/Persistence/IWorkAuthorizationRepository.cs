using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IWorkAuthorizationRepository
{
    Task<WorkAuthorization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkAuthorization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken);
    Task UpdateAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken);
    Task DeleteAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken);
}
