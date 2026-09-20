using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IJobApplicationService {

    Task Create(JobApplication model , CancellationToken cancellationToken);
    Task<bool> Update(JobApplication model, CancellationToken cancellationToken);
    Task<JobApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobApplication>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobApplicationService : IJobApplicationService
{
    private readonly IJobApplicationRepository _repository;
    private readonly ILogger<JobApplicationService> _logger;

    public JobApplicationService(
        IJobApplicationRepository repository, ILogger<JobApplicationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(JobApplication model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(JobApplication model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ApplicationNumber = model.ApplicationNumber;
            existing.AppliedDate = model.AppliedDate;
            existing.ResumeUrl = model.ResumeUrl;
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

    public Task<JobApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobApplication>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
