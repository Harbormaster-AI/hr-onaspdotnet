using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICandidateRepository
{
    Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Candidate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Candidate candidate, CancellationToken cancellationToken);
    Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken);
    Task DeleteAsync(Candidate candidate, CancellationToken cancellationToken);

    Task AddToApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInterviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInterviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOffersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOffersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDocumentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDocumentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
