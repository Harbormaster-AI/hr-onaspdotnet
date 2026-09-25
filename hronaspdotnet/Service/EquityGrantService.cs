
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface IEquityGrantService {

    Task Create(EquityGrant model , CancellationToken cancellationToken);
    Task<bool> Update(EquityGrant model, CancellationToken cancellationToken);
    Task<EquityGrant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EquityGrant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);


}

public class EquityGrantService : IEquityGrantService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IEquityGrantRepository _repository;
    private readonly ILogger<EquityGrantService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public EquityGrantService(
        ApplicationTelemetry telemetry,
        IEquityGrantRepository repository,
        ILogger<EquityGrantService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(EquityGrant model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "EquityGrant",
                "CreateEquityGrant",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(EquityGrant model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.GrantId = model.GrantId;
            existing.GrantedUnits = model.GrantedUnits;
            existing.VestingStart = model.VestingStart;
            existing.GrantType = model.GrantType;

            await _telemetry.Execute(
                "EquityGrant",
                "UpdateEquityGrant",
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

    public Task<EquityGrant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EquityGrant>> GetAll(CancellationToken cancellationToken)
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
                "EquityGrant",
                "UpdateEquityGrant",
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
            _logger.LogError("No EquityGrant found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No EquityGrant found using Id {ParentId}", request.ParentId);
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
