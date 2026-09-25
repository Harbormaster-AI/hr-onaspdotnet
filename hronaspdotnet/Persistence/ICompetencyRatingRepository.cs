using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICompetencyRatingRepository
{
    Task<CompetencyRating?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompetencyRating>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CompetencyRating competencyRating, CancellationToken cancellationToken);
    Task UpdateAsync(CompetencyRating competencyRating, CancellationToken cancellationToken);
    Task DeleteAsync(CompetencyRating competencyRating, CancellationToken cancellationToken);


}
