using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IBackgroundCheckRepository
{
    Task<BackgroundCheck?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BackgroundCheck>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken);
    Task UpdateAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken);
    Task DeleteAsync(BackgroundCheck backgroundCheck, CancellationToken cancellationToken);


}
