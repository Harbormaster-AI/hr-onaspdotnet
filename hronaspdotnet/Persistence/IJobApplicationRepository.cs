using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IJobApplicationRepository
{
    Task<JobApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobApplication>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(JobApplication jobApplication, CancellationToken cancellationToken);
    Task UpdateAsync(JobApplication jobApplication, CancellationToken cancellationToken);
    Task DeleteAsync(JobApplication jobApplication, CancellationToken cancellationToken);

    Task AddToScreeningsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromScreeningsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
