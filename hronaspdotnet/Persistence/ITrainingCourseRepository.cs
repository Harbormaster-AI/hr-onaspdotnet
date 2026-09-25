using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITrainingCourseRepository
{
    Task<TrainingCourse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainingCourse>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);
    Task UpdateAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);
    Task DeleteAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);

    Task AddToPrerequisitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPrerequisitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEnrollmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToJobProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromJobProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
