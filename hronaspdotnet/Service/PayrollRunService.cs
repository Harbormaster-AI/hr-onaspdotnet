
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPayrollRunRepository _repository;
    private readonly ILogger<PayrollRunService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PayrollRunService(
        ApplicationTelemetry telemetry,
        IPayrollRunRepository repository,
        ILogger<PayrollRunService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(PayrollRun model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "PayrollRun",
                "CreatePayrollRun",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "PayrollRun",
                "UpdatePayrollRun",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "PayrollRun",
                "UpdatePayrollRun",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PayrollRun found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PayrollCalendarService>().Get(childRequest, cancellationToken);
            parent.PayrollCalendar = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PayrollRun found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.PayrollCalendar = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "PayrollRun",
                "AddToPayrollItems",
                () => _repository.AddToPayrollItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPayrollItems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "PayrollRun",
                "RemoveFromPayrollItems",
                () => _repository.RemoveFromPayrollItemsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
