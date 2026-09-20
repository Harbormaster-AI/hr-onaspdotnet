using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPayrollCalendarService {

    Task Create(PayrollCalendar model , CancellationToken cancellationToken);
    Task<bool> Update(PayrollCalendar model, CancellationToken cancellationToken);
    Task<PayrollCalendar?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollCalendar>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPayrollRuns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayrollRuns(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PayrollCalendarService : IPayrollCalendarService
{
    private readonly IPayrollCalendarRepository _repository;
    private readonly ILogger<PayrollCalendarService> _logger;

    public PayrollCalendarService(
        IPayrollCalendarRepository repository, ILogger<PayrollCalendarService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PayrollCalendar model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PayrollCalendar model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Country = model.Country;
            existing.PayFrequency = model.PayFrequency;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PayrollCalendar?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PayrollCalendar>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToPayrollRuns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayrollRuns(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEmployees(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
