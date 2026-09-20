using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ICostCenterService {

    Task Create(CostCenter model , CancellationToken cancellationToken);
    Task<bool> Update(CostCenter model, CancellationToken cancellationToken);
    Task<CostCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CostCenter>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CostCenterService : ICostCenterService
{
    private readonly ICostCenterRepository _repository;
    private readonly ILogger<CostCenterService> _logger;

    public CostCenterService(
        ICostCenterRepository repository, ILogger<CostCenterService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CostCenter model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CostCenter model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Code = model.Code;
            existing.Name = model.Name;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CostCenter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CostCenter>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
