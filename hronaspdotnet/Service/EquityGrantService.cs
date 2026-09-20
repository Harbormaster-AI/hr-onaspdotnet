using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IEquityGrantService {

    Task Create(EquityGrant model , CancellationToken cancellationToken);
    Task<bool> Update(EquityGrant model, CancellationToken cancellationToken);
    Task<EquityGrant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EquityGrant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);


}

public class EquityGrantService : IEquityGrantService
{
    private readonly IEquityGrantRepository _repository;
    private readonly ILogger<EquityGrantService> _logger;

    public EquityGrantService(
        IEquityGrantRepository repository, ILogger<EquityGrantService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(EquityGrant model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(EquityGrant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.GrantId = model.GrantId;
            existing.GrantedUnits = model.GrantedUnits;
            existing.VestingStart = model.VestingStart;
            existing.GrantType = model.GrantType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<EquityGrant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EquityGrant>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
