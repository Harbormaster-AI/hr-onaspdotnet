using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ITimesheetService {

    Task Create(Timesheet model , CancellationToken cancellationToken);
    Task<bool> Update(Timesheet model, CancellationToken cancellationToken);
    Task<Timesheet?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Timesheet>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTimeEntries(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTimeEntries(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetRepository _repository;
    private readonly ILogger<TimesheetService> _logger;

    public TimesheetService(
        ITimesheetRepository repository, ILogger<TimesheetService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Timesheet model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Timesheet model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PeriodStart = model.PeriodStart;
            existing.PeriodEnd = model.PeriodEnd;
            existing.SubmissionDate = model.SubmissionDate;
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

    public Task<Timesheet?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Timesheet>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToTimeEntries(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTimeEntries(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApprovals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
