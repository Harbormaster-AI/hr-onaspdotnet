using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPerformanceReviewRepository
{
    Task<PerformanceReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceReview>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
    Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
    Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);

    Task AddToCompetencyRatingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCompetencyRatingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
