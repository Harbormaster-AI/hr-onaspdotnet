
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class GoalRepository : IGoalRepository
{
    private readonly ApplicationDbContext _db;

    public GoalRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Goal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Goals
            .Include(x => x.Employee)
            .Include(x => x.Cycle)
            .Include(x => x.ParentGoal)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Goal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Goals
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Cycle)
            .Include(x => x.ParentGoal)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Goal goal, CancellationToken cancellationToken)
    {
        _db.Goals.Add(goal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Goal goal, CancellationToken cancellationToken)
    {
        _db.Goals.Update(goal);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Goal goal, CancellationToken cancellationToken)
    {
        _db.Goals.Remove(goal);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToChildGoalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Goals
            .Where(goal =>
                request.ChildIds.Contains(goal.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goal =>
                        EF.Property<Guid?>(
                            goal,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromChildGoalsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Goals
            .Where(goal =>
                request.ChildIds.Contains(goal.Id) &&
                EF.Property<Guid?>(
                    goal,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    goal =>
                        EF.Property<Guid?>(
                            goal,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
