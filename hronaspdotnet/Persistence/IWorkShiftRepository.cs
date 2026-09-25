using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IWorkShiftRepository
{
    Task<WorkShift?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkShift>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(WorkShift workShift, CancellationToken cancellationToken);
    Task UpdateAsync(WorkShift workShift, CancellationToken cancellationToken);
    Task DeleteAsync(WorkShift workShift, CancellationToken cancellationToken);


}
