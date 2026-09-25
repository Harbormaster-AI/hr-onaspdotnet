using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITimesheetRepository
{
    Task<Timesheet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Timesheet>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Timesheet timesheet, CancellationToken cancellationToken);
    Task UpdateAsync(Timesheet timesheet, CancellationToken cancellationToken);
    Task DeleteAsync(Timesheet timesheet, CancellationToken cancellationToken);

    Task AddToTimeEntriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromTimeEntriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApprovalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
