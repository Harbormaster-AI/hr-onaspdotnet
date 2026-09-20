using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IWorkShiftService {

    Task Create(WorkShift model , CancellationToken cancellationToken);
    Task<bool> Update(WorkShift model, CancellationToken cancellationToken);
    Task<WorkShift?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkShift>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);


}

public class WorkShiftService : IWorkShiftService
{
    private readonly IWorkShiftRepository _repository;
    private readonly ILogger<WorkShiftService> _logger;

    public WorkShiftService(
        IWorkShiftRepository repository, ILogger<WorkShiftService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(WorkShift model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(WorkShift model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.StartTime = model.StartTime;
            existing.EndTime = model.EndTime;
            existing.BreakMinutes = model.BreakMinutes;
            existing.DayOfWeek = model.DayOfWeek;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<WorkShift?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<WorkShift>> GetAll(CancellationToken cancellationToken)
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




}
