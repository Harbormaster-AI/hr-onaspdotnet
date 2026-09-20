using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IEmployeeService {

    Task Create(Employee model , CancellationToken cancellationToken);
    Task<bool> Update(Employee model, CancellationToken cancellationToken);
    Task<Employee?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Employee>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPrimaryLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPrimaryLocation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEmploymentAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEmploymentAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBenefitEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBenefitEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTimesheets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTimesheets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPerformanceReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPerformanceReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTrainingEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrainingEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IEmployeeRepository repository, ILogger<EmployeeService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Employee model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Employee model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EmployeeNumber = model.EmployeeNumber;
            existing.Name = model.Name;
            existing.WorkEmail = model.WorkEmail;
            existing.WorkPhone = model.WorkPhone;
            existing.DateOfHire = model.DateOfHire;
            existing.NationalId = model.NationalId;
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

    public Task<Employee?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Employee>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPrimaryLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPrimaryLocation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCostCenter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDirectReports(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEmploymentAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEmploymentAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToBenefitEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBenefitEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTimesheets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTimesheets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLeaveRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPerformanceReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPerformanceReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTrainingEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrainingEnrollments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWorkAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkAuthorizations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
