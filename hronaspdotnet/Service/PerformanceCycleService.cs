using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPerformanceCycleService {

    Task Create(PerformanceCycle model , CancellationToken cancellationToken);
    Task<bool> Update(PerformanceCycle model, CancellationToken cancellationToken);
    Task<PerformanceCycle?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceCycle>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PerformanceCycleService : IPerformanceCycleService
{
    private readonly IPerformanceCycleRepository _repository;
    private readonly ILogger<PerformanceCycleService> _logger;

    public PerformanceCycleService(
        IPerformanceCycleRepository repository, ILogger<PerformanceCycleService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PerformanceCycle model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PerformanceCycle model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
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

    public Task<PerformanceCycle?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PerformanceCycle>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
