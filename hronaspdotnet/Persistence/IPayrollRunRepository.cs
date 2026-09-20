using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IPayrollRunRepository
{
    Task<PayrollRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollRun>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PayrollRun payrollRun, CancellationToken cancellationToken);
    Task UpdateAsync(PayrollRun payrollRun, CancellationToken cancellationToken);
    Task DeleteAsync(PayrollRun payrollRun, CancellationToken cancellationToken);
}
