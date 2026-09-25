
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _db;

    public EmployeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Employees
            .Include(x => x.Manager)
            .Include(x => x.Department)
            .Include(x => x.PrimaryLocation)
            .Include(x => x.CostCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Employees
            .AsNoTracking()
            .Include(x => x.Manager)
            .Include(x => x.Department)
            .Include(x => x.PrimaryLocation)
            .Include(x => x.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Update(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDirectReportsAsync(
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

    public async Task RemoveFromDirectReportsAsync(
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


    public async Task AddToEmploymentAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EmploymentAssignments
            .Where(employmentAssignment =>
                request.ChildIds.Contains(employmentAssignment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employmentAssignment =>
                        EF.Property<Guid?>(
                            employmentAssignment,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEmploymentAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EmploymentAssignments
            .Where(employmentAssignment =>
                request.ChildIds.Contains(employmentAssignment.Id) &&
                EF.Property<Guid?>(
                    employmentAssignment,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employmentAssignment =>
                        EF.Property<Guid?>(
                            employmentAssignment,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
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


    public async Task AddToBenefitEnrollmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BenefitEnrollments
            .Where(benefitEnrollment =>
                request.ChildIds.Contains(benefitEnrollment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    benefitEnrollment =>
                        EF.Property<Guid?>(
                            benefitEnrollment,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBenefitEnrollmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BenefitEnrollments
            .Where(benefitEnrollment =>
                request.ChildIds.Contains(benefitEnrollment.Id) &&
                EF.Property<Guid?>(
                    benefitEnrollment,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    benefitEnrollment =>
                        EF.Property<Guid?>(
                            benefitEnrollment,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToTimesheetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Timesheets
            .Where(timesheet =>
                request.ChildIds.Contains(timesheet.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    timesheet =>
                        EF.Property<Guid?>(
                            timesheet,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTimesheetsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Timesheets
            .Where(timesheet =>
                request.ChildIds.Contains(timesheet.Id) &&
                EF.Property<Guid?>(
                    timesheet,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    timesheet =>
                        EF.Property<Guid?>(
                            timesheet,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToLeaveRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LeaveRequests
            .Where(leaveRequest =>
                request.ChildIds.Contains(leaveRequest.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    leaveRequest =>
                        EF.Property<Guid?>(
                            leaveRequest,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLeaveRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LeaveRequests
            .Where(leaveRequest =>
                request.ChildIds.Contains(leaveRequest.Id) &&
                EF.Property<Guid?>(
                    leaveRequest,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    leaveRequest =>
                        EF.Property<Guid?>(
                            leaveRequest,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToPerformanceReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceReviews
            .Where(performanceReview =>
                request.ChildIds.Contains(performanceReview.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceReview =>
                        EF.Property<Guid?>(
                            performanceReview,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPerformanceReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PerformanceReviews
            .Where(performanceReview =>
                request.ChildIds.Contains(performanceReview.Id) &&
                EF.Property<Guid?>(
                    performanceReview,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    performanceReview =>
                        EF.Property<Guid?>(
                            performanceReview,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToTrainingEnrollmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrainingEnrollments
            .Where(trainingEnrollment =>
                request.ChildIds.Contains(trainingEnrollment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trainingEnrollment =>
                        EF.Property<Guid?>(
                            trainingEnrollment,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTrainingEnrollmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrainingEnrollments
            .Where(trainingEnrollment =>
                request.ChildIds.Contains(trainingEnrollment.Id) &&
                EF.Property<Guid?>(
                    trainingEnrollment,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trainingEnrollment =>
                        EF.Property<Guid?>(
                            trainingEnrollment,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToWorkAuthorizationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkAuthorizations
            .Where(workAuthorization =>
                request.ChildIds.Contains(workAuthorization.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workAuthorization =>
                        EF.Property<Guid?>(
                            workAuthorization,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkAuthorizationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.WorkAuthorizations
            .Where(workAuthorization =>
                request.ChildIds.Contains(workAuthorization.Id) &&
                EF.Property<Guid?>(
                    workAuthorization,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    workAuthorization =>
                        EF.Property<Guid?>(
                            workAuthorization,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
