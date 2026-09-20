using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IWorkScheduleService {

    Task Create(WorkSchedule model , CancellationToken cancellationToken);
    Task<bool> Update(WorkSchedule model, CancellationToken cancellationToken);
    Task<WorkSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkSchedule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToShifts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromShifts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class WorkScheduleService : IWorkScheduleService
{
    private readonly IWorkScheduleRepository _repository;
    private readonly ILogger<WorkScheduleService> _logger;

    public WorkScheduleService(
        IWorkScheduleRepository repository, ILogger<WorkScheduleService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(WorkSchedule model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(WorkSchedule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.StandardHoursPerWeek = model.StandardHoursPerWeek;
            existing.ScheduleType = model.ScheduleType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<WorkSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<WorkSchedule>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToShifts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromShifts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
