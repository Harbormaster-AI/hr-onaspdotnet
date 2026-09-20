using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IPerformanceReviewRepository
{
    Task<PerformanceReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceReview>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
    Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
    Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
}
