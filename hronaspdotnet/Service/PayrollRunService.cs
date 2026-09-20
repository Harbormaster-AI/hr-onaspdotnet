using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPayrollRunService {

    Task Create(PayrollRun model , CancellationToken cancellationToken);
    Task<bool> Update(PayrollRun model, CancellationToken cancellationToken);
    Task<PayrollRun?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PayrollRun>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PayrollRunService : IPayrollRunService
{
    private readonly IPayrollRunRepository _repository;
    private readonly ILogger<PayrollRunService> _logger;

    public PayrollRunService(
        IPayrollRunRepository repository, ILogger<PayrollRunService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PayrollRun model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PayrollRun model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RunNumber = model.RunNumber;
            existing.PeriodStart = model.PeriodStart;
            existing.PeriodEnd = model.PeriodEnd;
            existing.PaymentDate = model.PaymentDate;
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

    public Task<PayrollRun?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PayrollRun>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
