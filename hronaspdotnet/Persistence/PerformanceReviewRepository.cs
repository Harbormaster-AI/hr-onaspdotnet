
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class PerformanceReviewRepository : IPerformanceReviewRepository
{
    private readonly ApplicationDbContext _db;

    public PerformanceReviewRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PerformanceReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PerformanceReviews
            .Include(x => x.Employee)
            .Include(x => x.Reviewer)
            .Include(x => x.Cycle)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PerformanceReview>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PerformanceReviews
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Reviewer)
            .Include(x => x.Cycle)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Add(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Update(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
    {
        _db.PerformanceReviews.Remove(performanceReview);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCompetencyRatingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompetencyRatings
            .Where(competencyRating =>
                request.ChildIds.Contains(competencyRating.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    competencyRating =>
                        EF.Property<Guid?>(
                            competencyRating,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCompetencyRatingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompetencyRatings
            .Where(competencyRating =>
                request.ChildIds.Contains(competencyRating.Id) &&
                EF.Property<Guid?>(
                    competencyRating,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    competencyRating =>
                        EF.Property<Guid?>(
                            competencyRating,
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
