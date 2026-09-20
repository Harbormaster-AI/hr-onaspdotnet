using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IJobRequisitionService {

    Task Create(JobRequisition model , CancellationToken cancellationToken);
    Task<bool> Update(JobRequisition model, CancellationToken cancellationToken);
    Task<JobRequisition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobRequisition>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignHiringManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignHiringManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRecruiter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRecruiter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobRequisitionService : IJobRequisitionService
{
    private readonly IJobRequisitionRepository _repository;
    private readonly ILogger<JobRequisitionService> _logger;

    public JobRequisitionService(
        IJobRequisitionRepository repository, ILogger<JobRequisitionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(JobRequisition model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(JobRequisition model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RequisitionNumber = model.RequisitionNumber;
            existing.Title = model.Title;
            existing.Openings = model.Openings;
            existing.TargetStartDate = model.TargetStartDate;
            existing.Status = model.Status;
            existing.Priority = model.Priority;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<JobRequisition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobRequisition>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignHiringManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignHiringManager(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRecruiter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRecruiter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
