using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IEmploymentAssignmentService {

    Task Create(EmploymentAssignment model , CancellationToken cancellationToken);
    Task<bool> Update(EmploymentAssignment model, CancellationToken cancellationToken);
    Task<EmploymentAssignment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EmploymentAssignment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPosition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPosition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSupervisor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSupervisor(AssociationRequest request, CancellationToken cancellationToken);


}

public class EmploymentAssignmentService : IEmploymentAssignmentService
{
    private readonly IEmploymentAssignmentRepository _repository;
    private readonly ILogger<EmploymentAssignmentService> _logger;

    public EmploymentAssignmentService(
        IEmploymentAssignmentRepository repository, ILogger<EmploymentAssignmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(EmploymentAssignment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(EmploymentAssignment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Primary = model.Primary;
            existing.AssignmentType = model.AssignmentType;
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

    public Task<EmploymentAssignment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EmploymentAssignment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPosition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPosition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSupervisor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSupervisor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
