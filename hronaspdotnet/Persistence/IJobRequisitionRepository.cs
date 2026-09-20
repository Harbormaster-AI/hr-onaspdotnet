using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IJobRequisitionRepository
{
    Task<JobRequisition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobRequisition>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);
    Task UpdateAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);
    Task DeleteAsync(JobRequisition jobRequisition, CancellationToken cancellationToken);
}
