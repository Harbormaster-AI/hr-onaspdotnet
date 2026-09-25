
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface ICompensationPackageService {

    Task Create(CompensationPackage model , CancellationToken cancellationToken);
    Task<bool> Update(CompensationPackage model, CancellationToken cancellationToken);
    Task<CompensationPackage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompensationPackage>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CompensationPackageService : ICompensationPackageService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICompensationPackageRepository _repository;
    private readonly ILogger<CompensationPackageService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CompensationPackageService(
        ApplicationTelemetry telemetry,
        ICompensationPackageRepository repository,
        ILogger<CompensationPackageService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CompensationPackage model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CompensationPackage",
                "CreateCompensationPackage",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CompensationPackage model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EffectiveFrom = model.EffectiveFrom;
            existing.EffectiveTo = model.EffectiveTo;
            existing.Currency = model.Currency;

            await _telemetry.Execute(
                "CompensationPackage",
                "UpdateCompensationPackage",
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

    public Task<CompensationPackage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CompensationPackage>> GetAll(CancellationToken cancellationToken)
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
                "CompensationPackage",
                "UpdateCompensationPackage",
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

    public async Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CompensationPackage found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EmploymentContractService>().Get(childRequest, cancellationToken);
            parent.Contract = child;
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

    public async Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CompensationPackage found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Contract = null;
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


    public async Task<bool> AddToSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "AddToSalaryComponents",
                () => _repository.AddToSalaryComponentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSalaryComponents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "RemoveFromSalaryComponents",
                () => _repository.RemoveFromSalaryComponentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "AddToBonusPlans",
                () => _repository.AddToBonusPlansAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromBonusPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "RemoveFromBonusPlans",
                () => _repository.RemoveFromBonusPlansAsync(request, cancellationToken));
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

    public async Task<bool> AddToEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "AddToEquityGrants",
                () => _repository.AddToEquityGrantsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEquityGrants(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "CompensationPackage",
                "RemoveFromEquityGrants",
                () => _repository.RemoveFromEquityGrantsAsync(request, cancellationToken));
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
