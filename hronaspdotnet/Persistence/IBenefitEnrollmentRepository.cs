using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IBenefitEnrollmentRepository
{
    Task<BenefitEnrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BenefitEnrollment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);
    Task UpdateAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);
    Task DeleteAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken);
}
