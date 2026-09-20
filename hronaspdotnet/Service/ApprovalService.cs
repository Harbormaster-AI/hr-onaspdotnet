using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IApprovalService {

    Task Create(Approval model , CancellationToken cancellationToken);
    Task<bool> Update(Approval model, CancellationToken cancellationToken);
    Task<Approval?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Approval>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignApprover(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignApprover(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTimesheet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTimesheet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLeaveRequest(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLeaveRequest(AssociationRequest request, CancellationToken cancellationToken);


}

public class ApprovalService : IApprovalService
{
    private readonly IApprovalRepository _repository;
    private readonly ILogger<ApprovalService> _logger;

    public ApprovalService(
        IApprovalRepository repository, ILogger<ApprovalService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Approval model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Approval model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ApproverComment = model.ApproverComment;
            existing.ActionDate = model.ActionDate;
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

    public Task<Approval?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Approval>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignApprover(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignApprover(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTimesheet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTimesheet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLeaveRequest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLeaveRequest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
