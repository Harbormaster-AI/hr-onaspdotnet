using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ITrainingEnrollmentService {

    Task Create(TrainingEnrollment model , CancellationToken cancellationToken);
    Task<bool> Update(TrainingEnrollment model, CancellationToken cancellationToken);
    Task<TrainingEnrollment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TrainingEnrollment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCourse(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCourse(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignInstructor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInstructor(AssociationRequest request, CancellationToken cancellationToken);


}

public class TrainingEnrollmentService : ITrainingEnrollmentService
{
    private readonly ITrainingEnrollmentRepository _repository;
    private readonly ILogger<TrainingEnrollmentService> _logger;

    public TrainingEnrollmentService(
        ITrainingEnrollmentRepository repository, ILogger<TrainingEnrollmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TrainingEnrollment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(TrainingEnrollment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EnrollmentNumber = model.EnrollmentNumber;
            existing.CompletionDate = model.CompletionDate;
            existing.Score = model.Score;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<TrainingEnrollment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TrainingEnrollment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCourse(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCourse(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignInstructor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInstructor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
