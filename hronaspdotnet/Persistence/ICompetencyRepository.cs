using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICompetencyRepository
{
    Task<Competency?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Competency>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Competency competency, CancellationToken cancellationToken);
    Task UpdateAsync(Competency competency, CancellationToken cancellationToken);
    Task DeleteAsync(Competency competency, CancellationToken cancellationToken);

    Task AddToJobProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromJobProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCompetencyRatingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCompetencyRatingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
