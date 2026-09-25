using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Organization organization, CancellationToken cancellationToken);
    Task UpdateAsync(Organization organization, CancellationToken cancellationToken);
    Task DeleteAsync(Organization organization, CancellationToken cancellationToken);

    Task AddToDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToJobFamiliesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromJobFamiliesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBenefitPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBenefitPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCostCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCostCentersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPayrollCalendarsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayrollCalendarsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
