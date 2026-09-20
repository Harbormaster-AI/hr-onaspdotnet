using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IEmploymentContractService {

    Task Create(EmploymentContract model , CancellationToken cancellationToken);
    Task<bool> Update(EmploymentContract model, CancellationToken cancellationToken);
    Task<EmploymentContract?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EmploymentContract>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken);


}

public class EmploymentContractService : IEmploymentContractService
{
    private readonly IEmploymentContractRepository _repository;
    private readonly ILogger<EmploymentContractService> _logger;

    public EmploymentContractService(
        IEmploymentContractRepository repository, ILogger<EmploymentContractService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(EmploymentContract model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(EmploymentContract model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ContractNumber = model.ContractNumber;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.WorkHoursPerWeek = model.WorkHoursPerWeek;
            existing.EmploymentType = model.EmploymentType;
            existing.Status = model.Status;
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

    public Task<EmploymentContract?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<EmploymentContract>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCompensationPackage(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPayrollCalendar(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
