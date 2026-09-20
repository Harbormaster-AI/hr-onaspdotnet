using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IWorkScheduleRepository
{
    Task<WorkSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);
}
