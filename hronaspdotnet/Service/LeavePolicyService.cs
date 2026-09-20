using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ILeavePolicyService {

    Task Create(LeavePolicy model , CancellationToken cancellationToken);
    Task<bool> Update(LeavePolicy model, CancellationToken cancellationToken);
    Task<LeavePolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<LeavePolicy>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class LeavePolicyService : ILeavePolicyService
{
    private readonly ILeavePolicyRepository _repository;
    private readonly ILogger<LeavePolicyService> _logger;

    public LeavePolicyService(
        ILeavePolicyRepository repository, ILogger<LeavePolicyService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(LeavePolicy model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(LeavePolicy model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.AccrualRate = model.AccrualRate;
            existing.CarryoverAllowed = model.CarryoverAllowed;
            existing.MaxBalance = model.MaxBalance;
            existing.LeaveCategory = model.LeaveCategory;
            existing.AccrualUnit = model.AccrualUnit;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<LeavePolicy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<LeavePolicy>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
