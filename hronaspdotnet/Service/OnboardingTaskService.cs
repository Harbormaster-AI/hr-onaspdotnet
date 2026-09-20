using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IOnboardingTaskService {

    Task Create(OnboardingTask model , CancellationToken cancellationToken);
    Task<bool> Update(OnboardingTask model, CancellationToken cancellationToken);
    Task<OnboardingTask?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<OnboardingTask>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAssignedTo(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAssignedTo(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRelatedOffer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRelatedOffer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDependencies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDependencies(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OnboardingTaskService : IOnboardingTaskService
{
    private readonly IOnboardingTaskRepository _repository;
    private readonly ILogger<OnboardingTaskService> _logger;

    public OnboardingTaskService(
        IOnboardingTaskRepository repository, ILogger<OnboardingTaskService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(OnboardingTask model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(OnboardingTask model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.TaskNumber = model.TaskNumber;
            existing.Name = model.Name;
            existing.DueDate = model.DueDate;
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

    public Task<OnboardingTask?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<OnboardingTask>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAssignedTo(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAssignedTo(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRelatedOffer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRelatedOffer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDependencies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDependencies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
