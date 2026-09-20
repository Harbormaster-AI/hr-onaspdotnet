using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ITimesheetRepository
{
    Task<Timesheet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Timesheet>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Timesheet timesheet, CancellationToken cancellationToken);
    Task UpdateAsync(Timesheet timesheet, CancellationToken cancellationToken);
    Task DeleteAsync(Timesheet timesheet, CancellationToken cancellationToken);
}
