using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPayrollItemRepository
{
    Task<PayrollItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PayrollItem payrollItem, CancellationToken cancellationToken);
    Task UpdateAsync(PayrollItem payrollItem, CancellationToken cancellationToken);
    Task DeleteAsync(PayrollItem payrollItem, CancellationToken cancellationToken);


}
