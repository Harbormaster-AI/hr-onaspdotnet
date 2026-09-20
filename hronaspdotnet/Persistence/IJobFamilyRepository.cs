using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IJobFamilyRepository
{
    Task<JobFamily?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobFamily>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(JobFamily jobFamily, CancellationToken cancellationToken);
    Task UpdateAsync(JobFamily jobFamily, CancellationToken cancellationToken);
    Task DeleteAsync(JobFamily jobFamily, CancellationToken cancellationToken);
}
