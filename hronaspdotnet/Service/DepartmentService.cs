using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IDepartmentService {

    Task Create(Department model , CancellationToken cancellationToken);
    Task<bool> Update(Department model, CancellationToken cancellationToken);
    Task<Department?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Department>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(
        IDepartmentRepository repository, ILogger<DepartmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Department model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Department model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Code = model.Code;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Department?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Department>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
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
