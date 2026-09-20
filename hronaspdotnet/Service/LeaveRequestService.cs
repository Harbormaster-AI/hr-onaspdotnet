using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ILeaveRequestService {

    Task Create(LeaveRequest model , CancellationToken cancellationToken);
    Task<bool> Update(LeaveRequest model, CancellationToken cancellationToken);
    Task<LeaveRequest?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LeaveRequest>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLeavePolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLeavePolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _repository;
    private readonly ILogger<LeaveRequestService> _logger;

    public LeaveRequestService(
        ILeaveRequestRepository repository, ILogger<LeaveRequestService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LeaveRequest model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LeaveRequest model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RequestNumber = model.RequestNumber;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Reason = model.Reason;
            existing.Hours = model.Hours;
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

    public Task<LeaveRequest?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LeaveRequest>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignLeavePolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLeavePolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
