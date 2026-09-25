using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPayrollRunRepository
{
    Task<PayrollRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollRun>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PayrollRun payrollRun, CancellationToken cancellationToken);
    Task UpdateAsync(PayrollRun payrollRun, CancellationToken cancellationToken);
    Task DeleteAsync(PayrollRun payrollRun, CancellationToken cancellationToken);

    Task AddToPayrollItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayrollItemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
