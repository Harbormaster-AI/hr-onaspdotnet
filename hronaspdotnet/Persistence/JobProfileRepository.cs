
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class JobProfileRepository : IJobProfileRepository
{
    private readonly ApplicationDbContext _db;

    public JobProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<JobProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.JobProfiles
            .Include(x => x.JobFamily)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<JobProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.JobProfiles
            .AsNoTracking()
            .Include(x => x.JobFamily)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Add(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Update(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Remove(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCompetenciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Competencys
            .Where(competency =>
                request.ChildIds.Contains(competency.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    competency =>
                        EF.Property<Guid?>(
                            competency,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCompetenciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Competencys
            .Where(competency =>
                request.ChildIds.Contains(competency.Id) &&
                EF.Property<Guid?>(
                    competency,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    competency =>
                        EF.Property<Guid?>(
                            competency,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToTrainingRecommendationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrainingCourses
            .Where(trainingCourse =>
                request.ChildIds.Contains(trainingCourse.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trainingCourse =>
                        EF.Property<Guid?>(
                            trainingCourse,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTrainingRecommendationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TrainingCourses
            .Where(trainingCourse =>
                request.ChildIds.Contains(trainingCourse.Id) &&
                EF.Property<Guid?>(
                    trainingCourse,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    trainingCourse =>
                        EF.Property<Guid?>(
                            trainingCourse,
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

}
