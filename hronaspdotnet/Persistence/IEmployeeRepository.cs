using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Employee employee, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken);

    Task AddToDirectReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDirectReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEmploymentAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEmploymentAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBenefitEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBenefitEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTimesheetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTimesheetsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLeaveRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLeaveRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPerformanceReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPerformanceReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToTrainingEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTrainingEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWorkAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkAuthorizationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
