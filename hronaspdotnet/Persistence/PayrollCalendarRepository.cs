
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PayrollCalendarRepository : IPayrollCalendarRepository
{
    private readonly ApplicationDbContext _db;

    public PayrollCalendarRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PayrollCalendar?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PayrollCalendars
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PayrollCalendar>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PayrollCalendars
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Add(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Update(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PayrollCalendar payrollCalendar, CancellationToken cancellationToken)
    {
        _db.PayrollCalendars.Remove(payrollCalendar);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPayrollRunsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PayrollRuns
            .Where(payrollRun =>
                request.ChildIds.Contains(payrollRun.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payrollRun =>
                        EF.Property<Guid?>(
                            payrollRun,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPayrollRunsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PayrollRuns
            .Where(payrollRun =>
                request.ChildIds.Contains(payrollRun.Id) &&
                EF.Property<Guid?>(
                    payrollRun,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payrollRun =>
                        EF.Property<Guid?>(
                            payrollRun,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToEmployeesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Employees
            .Where(employee =>
                request.ChildIds.Contains(employee.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employee =>
                        EF.Property<Guid?>(
                            employee,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEmployeesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Employees
            .Where(employee =>
                request.ChildIds.Contains(employee.Id) &&
                EF.Property<Guid?>(
                    employee,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employee =>
                        EF.Property<Guid?>(
                            employee,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
