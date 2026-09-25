
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _db;

    public PositionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Positions
            .Include(x => x.Department)
            .Include(x => x.JobProfile)
            .Include(x => x.CostCenter)
            .Include(x => x.Location)
            .Include(x => x.ManagerPosition)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Position>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Positions
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.JobProfile)
            .Include(x => x.CostCenter)
            .Include(x => x.Location)
            .Include(x => x.ManagerPosition)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Position position, CancellationToken cancellationToken)
    {
        _db.Positions.Add(position);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Position position, CancellationToken cancellationToken)
    {
        _db.Positions.Update(position);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Position position, CancellationToken cancellationToken)
    {
        _db.Positions.Remove(position);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDirectReportsAsync(
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

    public async Task RemoveFromDirectReportsAsync(
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


    public async Task AddToAssignmentsAsync(
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

    public async Task RemoveFromAssignmentsAsync(
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

}
