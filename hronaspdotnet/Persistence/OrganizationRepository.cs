
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _db;

    public OrganizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Add(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Update(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Remove(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDepartmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Departments
            .Where(department =>
                request.ChildIds.Contains(department.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    department =>
                        EF.Property<Guid?>(
                            department,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDepartmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Departments
            .Where(department =>
                request.ChildIds.Contains(department.Id) &&
                EF.Property<Guid?>(
                    department,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    department =>
                        EF.Property<Guid?>(
                            department,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToLocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Locations
            .Where(location =>
                request.ChildIds.Contains(location.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    location =>
                        EF.Property<Guid?>(
                            location,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Locations
            .Where(location =>
                request.ChildIds.Contains(location.Id) &&
                EF.Property<Guid?>(
                    location,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    location =>
                        EF.Property<Guid?>(
                            location,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToJobFamiliesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobFamilys
            .Where(jobFamily =>
                request.ChildIds.Contains(jobFamily.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobFamily =>
                        EF.Property<Guid?>(
                            jobFamily,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromJobFamiliesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobFamilys
            .Where(jobFamily =>
                request.ChildIds.Contains(jobFamily.Id) &&
                EF.Property<Guid?>(
                    jobFamily,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobFamily =>
                        EF.Property<Guid?>(
                            jobFamily,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToBenefitPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BenefitPlans
            .Where(benefitPlan =>
                request.ChildIds.Contains(benefitPlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    benefitPlan =>
                        EF.Property<Guid?>(
                            benefitPlan,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBenefitPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BenefitPlans
            .Where(benefitPlan =>
                request.ChildIds.Contains(benefitPlan.Id) &&
                EF.Property<Guid?>(
                    benefitPlan,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    benefitPlan =>
                        EF.Property<Guid?>(
                            benefitPlan,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToCostCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CostCenters
            .Where(costCenter =>
                request.ChildIds.Contains(costCenter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    costCenter =>
                        EF.Property<Guid?>(
                            costCenter,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCostCentersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CostCenters
            .Where(costCenter =>
                request.ChildIds.Contains(costCenter.Id) &&
                EF.Property<Guid?>(
                    costCenter,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    costCenter =>
                        EF.Property<Guid?>(
                            costCenter,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToPayrollCalendarsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PayrollCalendars
            .Where(payrollCalendar =>
                request.ChildIds.Contains(payrollCalendar.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payrollCalendar =>
                        EF.Property<Guid?>(
                            payrollCalendar,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPayrollCalendarsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PayrollCalendars
            .Where(payrollCalendar =>
                request.ChildIds.Contains(payrollCalendar.Id) &&
                EF.Property<Guid?>(
                    payrollCalendar,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payrollCalendar =>
                        EF.Property<Guid?>(
                            payrollCalendar,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
