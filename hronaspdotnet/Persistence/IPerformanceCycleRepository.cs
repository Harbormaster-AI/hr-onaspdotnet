using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPerformanceCycleRepository
{
    Task<PerformanceCycle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceCycle>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken);
    Task UpdateAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken);
    Task DeleteAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken);

    Task AddToReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
