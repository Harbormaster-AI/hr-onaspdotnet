using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ICompetencyRepository
{
    Task<Competency?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Competency>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Competency competency, CancellationToken cancellationToken);
    Task UpdateAsync(Competency competency, CancellationToken cancellationToken);
    Task DeleteAsync(Competency competency, CancellationToken cancellationToken);
}
