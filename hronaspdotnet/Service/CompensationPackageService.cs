using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ICompensationPackageService {

    Task Create(CompensationPackage model , CancellationToken cancellationToken);
    Task<bool> Update(CompensationPackage model, CancellationToken cancellationToken);
    Task<CompensationPackage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompensationPackage>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CompensationPackageService : ICompensationPackageService
{
    private readonly ICompensationPackageRepository _repository;
    private readonly ILogger<CompensationPackageService> _logger;

    public CompensationPackageService(
        ICompensationPackageRepository repository, ILogger<CompensationPackageService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CompensationPackage model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CompensationPackage model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EffectiveFrom = model.EffectiveFrom;
            existing.EffectiveTo = model.EffectiveTo;
            existing.Currency = model.Currency;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CompensationPackage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CompensationPackage>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
