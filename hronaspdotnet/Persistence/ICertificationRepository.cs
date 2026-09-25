using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICertificationRepository
{
    Task<Certification?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Certification>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Certification certification, CancellationToken cancellationToken);
    Task UpdateAsync(Certification certification, CancellationToken cancellationToken);
    Task DeleteAsync(Certification certification, CancellationToken cancellationToken);


}
