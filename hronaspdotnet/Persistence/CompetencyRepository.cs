
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CompetencyRepository : ICompetencyRepository
{
    private readonly ApplicationDbContext _db;

    public CompetencyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Competency?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Competencys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Competency>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Competencys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Competency competency, CancellationToken cancellationToken)
    {
        _db.Competencys.Add(competency);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Competency competency, CancellationToken cancellationToken)
    {
        _db.Competencys.Update(competency);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Competency competency, CancellationToken cancellationToken)
    {
        _db.Competencys.Remove(competency);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToJobProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobProfiles
            .Where(jobProfile =>
                request.ChildIds.Contains(jobProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobProfile =>
                        EF.Property<Guid?>(
                            jobProfile,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromJobProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobProfiles
            .Where(jobProfile =>
                request.ChildIds.Contains(jobProfile.Id) &&
                EF.Property<Guid?>(
                    jobProfile,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobProfile =>
                        EF.Property<Guid?>(
                            jobProfile,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
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

}
