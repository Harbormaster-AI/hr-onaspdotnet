
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CostCenterRepository : ICostCenterRepository
{
    private readonly ApplicationDbContext _db;

    public CostCenterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CostCenters
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CostCenter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CostCenters
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Add(costCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Update(costCenter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CostCenter costCenter, CancellationToken cancellationToken)
    {
        _db.CostCenters.Remove(costCenter);
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


    public async Task AddToPositionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Positions
            .Where(position =>
                request.ChildIds.Contains(position.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    position =>
                        EF.Property<Guid?>(
                            position,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPositionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Positions
            .Where(position =>
                request.ChildIds.Contains(position.Id) &&
                EF.Property<Guid?>(
                    position,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    position =>
                        EF.Property<Guid?>(
                            position,
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
