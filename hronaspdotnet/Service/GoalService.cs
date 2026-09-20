using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IGoalService {

    Task Create(Goal model , CancellationToken cancellationToken);
    Task<bool> Update(Goal model, CancellationToken cancellationToken);
    Task<Goal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Goal>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCycle(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCycle(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignParentGoal(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignParentGoal(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChildGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChildGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class GoalService : IGoalService
{
    private readonly IGoalRepository _repository;
    private readonly ILogger<GoalService> _logger;

    public GoalService(
        IGoalRepository repository, ILogger<GoalService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Goal model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Goal model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.TargetDate = model.TargetDate;
            existing.Weight = model.Weight;
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

    public Task<Goal?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Goal>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCycle(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCycle(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignParentGoal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignParentGoal(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToChildGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromChildGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
