
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class WorkScheduleRepository : IWorkScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public WorkScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkSchedules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Add(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Update(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkSchedule workSchedule, CancellationToken cancellationToken)
    {
        _db.WorkSchedules.Remove(workSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EmploymentContracts
            .Where(employmentContract =>
                request.ChildIds.Contains(employmentContract.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employmentContract =>
                        EF.Property<Guid?>(
                            employmentContract,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EmploymentContracts
            .Where(employmentContract =>
                request.ChildIds.Contains(employmentContract.Id) &&
                EF.Property<Guid?>(
                    employmentContract,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employmentContract =>
                        EF.Property<Guid?>(
                            employmentContract,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToShiftsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkShifts
            .Where(workShift =>
                request.ChildIds.Contains(workShift.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workShift =>
                        EF.Property<Guid?>(
                            workShift,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromShiftsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkShifts
            .Where(workShift =>
                request.ChildIds.Contains(workShift.Id) &&
                EF.Property<Guid?>(
                    workShift,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workShift =>
                        EF.Property<Guid?>(
                            workShift,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ScheduleExceptions
            .Where(scheduleException =>
                request.ChildIds.Contains(scheduleException.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    scheduleException =>
                        EF.Property<Guid?>(
                            scheduleException,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ScheduleExceptions
            .Where(scheduleException =>
                request.ChildIds.Contains(scheduleException.Id) &&
                EF.Property<Guid?>(
                    scheduleException,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    scheduleException =>
                        EF.Property<Guid?>(
                            scheduleException,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
