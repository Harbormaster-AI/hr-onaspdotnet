
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TrainingCourseRepository : ITrainingCourseRepository
{
    private readonly ApplicationDbContext _db;

    public TrainingCourseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TrainingCourse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TrainingCourses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TrainingCourse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TrainingCourses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Add(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Update(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Remove(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPrerequisitesAsync(
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

    public async Task RemoveFromPrerequisitesAsync(
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


    public async Task AddToEnrollmentsAsync(
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

    public async Task RemoveFromEnrollmentsAsync(
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

}
