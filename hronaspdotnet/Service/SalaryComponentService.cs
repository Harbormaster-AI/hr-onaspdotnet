using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ISalaryComponentService {

    Task Create(SalaryComponent model , CancellationToken cancellationToken);
    Task<bool> Update(SalaryComponent model, CancellationToken cancellationToken);
    Task<SalaryComponent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalaryComponent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);


}

public class SalaryComponentService : ISalaryComponentService
{
    private readonly ISalaryComponentRepository _repository;
    private readonly ILogger<SalaryComponentService> _logger;

    public SalaryComponentService(
        ISalaryComponentRepository repository, ILogger<SalaryComponentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SalaryComponent model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(SalaryComponent model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Amount = model.Amount;
            existing.Recurring = model.Recurring;
            existing.ComponentType = model.ComponentType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<SalaryComponent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SalaryComponent>> GetAll(CancellationToken cancellationToken)
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
