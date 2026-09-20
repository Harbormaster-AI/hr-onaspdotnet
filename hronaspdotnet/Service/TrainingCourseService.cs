using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ITrainingCourseService {

    Task Create(TrainingCourse model , CancellationToken cancellationToken);
    Task<bool> Update(TrainingCourse model, CancellationToken cancellationToken);
    Task<TrainingCourse?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainingCourse>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToPrerequisites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPrerequisites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TrainingCourseService : ITrainingCourseService
{
    private readonly ITrainingCourseRepository _repository;
    private readonly ILogger<TrainingCourseService> _logger;

    public TrainingCourseService(
        ITrainingCourseRepository repository, ILogger<TrainingCourseService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TrainingCourse model, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(TrainingCourse model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Code = model.Code;
            existing.Title = model.Title;
            existing.DurationHours = model.DurationHours;
            existing.DeliveryMethod = model.DeliveryMethod;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<TrainingCourse?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TrainingCourse>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }


    public async Task<bool> AddToPrerequisites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPrerequisites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
