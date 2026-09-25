using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITrainingEnrollmentRepository
{
    Task<TrainingEnrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainingEnrollment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken);
    Task UpdateAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken);
    Task DeleteAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken);


}
