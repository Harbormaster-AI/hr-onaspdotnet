using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IJobRequisitionRepository
{
    Task<JobRequisition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobRequisition>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);
    Task UpdateAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);
    Task DeleteAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);

    Task AddToCandidatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCandidatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInterviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInterviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOffersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOffersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
