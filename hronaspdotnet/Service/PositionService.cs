using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPositionService {

    Task Create(Position model , CancellationToken cancellationToken);
    Task<bool> Update(Position model, CancellationToken cancellationToken);
    Task<Position?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Position>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignManagerPosition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManagerPosition(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PositionService : IPositionService
{
    private readonly IPositionRepository _repository;
    private readonly ILogger<PositionService> _logger;

    public PositionService(
        IPositionRepository repository, ILogger<PositionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Position model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Position model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PositionCode = model.PositionCode;
            existing.Fte = model.Fte;
            existing.Status = model.Status;
            existing.WorkLocationType = model.WorkLocationType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Position?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Position>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignManagerPosition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManagerPosition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
