using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IBenefitEnrollmentService {

    Task Create(BenefitEnrollment model , CancellationToken cancellationToken);
    Task<bool> Update(BenefitEnrollment model, CancellationToken cancellationToken);
    Task<BenefitEnrollment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BenefitEnrollment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBenefitPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBenefitPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDependents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDependents(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BenefitEnrollmentService : IBenefitEnrollmentService
{
    private readonly IBenefitEnrollmentRepository _repository;
    private readonly ILogger<BenefitEnrollmentService> _logger;

    public BenefitEnrollmentService(
        IBenefitEnrollmentRepository repository, ILogger<BenefitEnrollmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BenefitEnrollment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BenefitEnrollment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EnrollmentId = model.EnrollmentId;
            existing.EffectiveFrom = model.EffectiveFrom;
            existing.EffectiveTo = model.EffectiveTo;
            existing.Status = model.Status;
            existing.CoverageLevel = model.CoverageLevel;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<BenefitEnrollment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BenefitEnrollment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBenefitPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBenefitPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDependents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDependents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
