
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PerformanceCycleRepository : IPerformanceCycleRepository
{
    private readonly ApplicationDbContext _db;

    public PerformanceCycleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PerformanceCycle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PerformanceCycles
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PerformanceCycle>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PerformanceCycles
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Add(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Update(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PerformanceCycle performanceCycle, CancellationToken cancellationToken)
    {
        _db.PerformanceCycles.Remove(performanceCycle);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToReviewsAsync(
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

    public async Task RemoveFromReviewsAsync(
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


    public async Task AddToGoalsAsync(
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

    public async Task RemoveFromGoalsAsync(
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
