using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ITrainingCourseRepository
{
    Task<TrainingCourse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainingCourse>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);
    Task UpdateAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);
    Task DeleteAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken);
}
