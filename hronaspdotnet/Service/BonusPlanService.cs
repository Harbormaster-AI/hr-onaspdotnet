using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IBonusPlanService {

    Task Create(BonusPlan model , CancellationToken cancellationToken);
    Task<bool> Update(BonusPlan model, CancellationToken cancellationToken);
    Task<BonusPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BonusPlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToCompensationPackages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompensationPackages(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BonusPlanService : IBonusPlanService
{
    private readonly IBonusPlanRepository _repository;
    private readonly ILogger<BonusPlanService> _logger;

    public BonusPlanService(
        IBonusPlanRepository repository, ILogger<BonusPlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BonusPlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BonusPlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TargetPercentage = model.TargetPercentage;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BonusPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BonusPlan>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToCompensationPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompensationPackages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
