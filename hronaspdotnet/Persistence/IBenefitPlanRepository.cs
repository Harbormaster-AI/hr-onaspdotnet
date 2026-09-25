using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IBenefitPlanRepository
{
    Task<BenefitPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BenefitPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken);
    Task UpdateAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken);
    Task DeleteAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken);

    Task AddToEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
