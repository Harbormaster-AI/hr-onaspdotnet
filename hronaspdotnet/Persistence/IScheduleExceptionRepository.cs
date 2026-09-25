using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IScheduleExceptionRepository
{
    Task<ScheduleException?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleException>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ScheduleException scheduleException, CancellationToken cancellationToken);
    Task UpdateAsync(ScheduleException scheduleException, CancellationToken cancellationToken);
    Task DeleteAsync(ScheduleException scheduleException, CancellationToken cancellationToken);


}
