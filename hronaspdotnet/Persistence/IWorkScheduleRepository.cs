using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IWorkScheduleRepository
{
    Task<WorkSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(WorkSchedule workSchedule, CancellationToken cancellationToken);

    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToShiftsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromShiftsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
