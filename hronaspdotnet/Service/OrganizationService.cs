using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IOrganizationService {

    Task Create(Organization model , CancellationToken cancellationToken);
    Task<bool> Update(Organization model, CancellationToken cancellationToken);
    Task<Organization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Organization>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToJobFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromJobFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBenefitPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBenefitPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCostCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCostCenters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPayrollCalendars(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPayrollCalendars(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _repository;
    private readonly ILogger<OrganizationService> _logger;

    public OrganizationService(
        IOrganizationRepository repository, ILogger<OrganizationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Organization model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Organization model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.RegistrationCountry = model.RegistrationCountry;
            existing.Website = model.Website;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Organization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Organization>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDepartments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToJobFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromJobFamilies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToBenefitPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBenefitPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCostCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCostCenters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPayrollCalendars(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPayrollCalendars(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
