using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IBenefitPlanService {

    Task Create(BenefitPlan model , CancellationToken cancellationToken);
    Task<bool> Update(BenefitPlan model, CancellationToken cancellationToken);
    Task<BenefitPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BenefitPlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BenefitPlanService : IBenefitPlanService
{
    private readonly IBenefitPlanRepository _repository;
    private readonly ILogger<BenefitPlanService> _logger;

    public BenefitPlanService(
        IBenefitPlanRepository repository, ILogger<BenefitPlanService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BenefitPlan model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BenefitPlan model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ProviderName = model.ProviderName;
            existing.EmployeeContributionRate = model.EmployeeContributionRate;
            existing.EmployerContributionRate = model.EmployerContributionRate;
            existing.EligibilityRules = model.EligibilityRules;
            existing.BenefitType = model.BenefitType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BenefitPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BenefitPlan>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
