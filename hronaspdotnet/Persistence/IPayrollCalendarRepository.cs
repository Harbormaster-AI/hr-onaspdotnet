using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPayrollCalendarRepository
{
    Task<PayrollCalendar?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollCalendar>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken);
    Task UpdateAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken);
    Task DeleteAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken);

    Task AddToPayrollRunsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPayrollRunsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
