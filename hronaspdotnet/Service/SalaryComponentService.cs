
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface ISalaryComponentService {

    Task Create(SalaryComponent model , CancellationToken cancellationToken);
    Task<bool> Update(SalaryComponent model, CancellationToken cancellationToken);
    Task<SalaryComponent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalaryComponent>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);


}

public class SalaryComponentService : ISalaryComponentService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ISalaryComponentRepository _repository;
    private readonly ILogger<SalaryComponentService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public SalaryComponentService(
        ApplicationTelemetry telemetry,
        ISalaryComponentRepository repository,
        ILogger<SalaryComponentService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(SalaryComponent model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "SalaryComponent",
                "CreateSalaryComponent",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(SalaryComponent model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Amount = model.Amount;
            existing.Recurring = model.Recurring;
            existing.ComponentType = model.ComponentType;

            await _telemetry.Execute(
                "SalaryComponent",
                "UpdateSalaryComponent",
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

    public Task<SalaryComponent?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SalaryComponent>> GetAll(CancellationToken cancellationToken)
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
                "SalaryComponent",
                "UpdateSalaryComponent",
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

    public async Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SalaryComponent found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CompensationPackageService>().Get(childRequest, cancellationToken);
            parent.CompensationPackage = child;
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

    public async Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No SalaryComponent found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.CompensationPackage = null;
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




}
