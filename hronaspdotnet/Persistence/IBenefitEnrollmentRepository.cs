using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IBenefitEnrollmentRepository
{
    Task<BenefitEnrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BenefitEnrollment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);
    Task UpdateAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);
    Task DeleteAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);

    Task AddToDependentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDependentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
