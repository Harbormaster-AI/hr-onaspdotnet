using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ICandidateRepository
{
    Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Candidate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Candidate candidate, CancellationToken cancellationToken);
    Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken);
    Task DeleteAsync(Candidate candidate, CancellationToken cancellationToken);
}
