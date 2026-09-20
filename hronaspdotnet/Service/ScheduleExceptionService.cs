using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IScheduleExceptionService {

    Task Create(ScheduleException model , CancellationToken cancellationToken);
    Task<bool> Update(ScheduleException model, CancellationToken cancellationToken);
    Task<ScheduleException?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleException>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);


}

public class ScheduleExceptionService : IScheduleExceptionService
{
    private readonly IScheduleExceptionRepository _repository;
    private readonly ILogger<ScheduleExceptionService> _logger;

    public ScheduleExceptionService(
        IScheduleExceptionRepository repository, ILogger<ScheduleExceptionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ScheduleException model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ScheduleException model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Date = model.Date;
            existing.Reason = model.Reason;
            existing.Hours = model.Hours;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ScheduleException?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ScheduleException>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
