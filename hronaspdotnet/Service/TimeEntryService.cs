using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ITimeEntryService {

    Task Create(TimeEntry model , CancellationToken cancellationToken);
    Task<bool> Update(TimeEntry model, CancellationToken cancellationToken);
    Task<TimeEntry?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<TimeEntry>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTimesheet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTimesheet(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken);


}

public class TimeEntryService : ITimeEntryService
{
    private readonly ITimeEntryRepository _repository;
    private readonly ILogger<TimeEntryService> _logger;

    public TimeEntryService(
        ITimeEntryRepository repository, ILogger<TimeEntryService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(TimeEntry model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(TimeEntry model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EntryDate = model.EntryDate;
            existing.HoursWorked = model.HoursWorked;
            existing.EntryType = model.EntryType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<TimeEntry?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<TimeEntry>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTimesheet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTimesheet(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
