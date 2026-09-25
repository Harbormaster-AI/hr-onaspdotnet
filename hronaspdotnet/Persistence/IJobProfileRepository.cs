using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IJobProfileRepository
{
    Task<JobProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(JobProfile jobProfile, CancellationToken cancellationToken);
    Task UpdateAsync(JobProfile jobProfile, CancellationToken cancellationToken);
    Task DeleteAsync(JobProfile jobProfile, CancellationToken cancellationToken);

    Task AddToCompetenciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCompetenciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTrainingRecommendationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTrainingRecommendationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
