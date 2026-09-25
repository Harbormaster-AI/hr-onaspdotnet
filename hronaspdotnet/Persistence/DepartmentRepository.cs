
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _db;

    public DepartmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Departments
            .Include(x => x.Organization)
            .Include(x => x.Manager)
            .Include(x => x.CostCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Departments
            .AsNoTracking()
            .Include(x => x.Organization)
            .Include(x => x.Manager)
            .Include(x => x.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Add(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Update(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Remove(department);
        await _db.SaveChangesAsync(cancellationToken);
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
