using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITimeEntryRepository
{
    Task<TimeEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TimeEntry>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TimeEntry timeEntry, CancellationToken cancellationToken);
    Task UpdateAsync(TimeEntry timeEntry, CancellationToken cancellationToken);
    Task DeleteAsync(TimeEntry timeEntry, CancellationToken cancellationToken);


}
