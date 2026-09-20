using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IInterviewService {

    Task Create(Interview model , CancellationToken cancellationToken);
    Task<bool> Update(Interview model, CancellationToken cancellationToken);
    Task<Interview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Interview>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToInterviewers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInterviewers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InterviewService : IInterviewService
{
    private readonly IInterviewRepository _repository;
    private readonly ILogger<InterviewService> _logger;

    public InterviewService(
        IInterviewRepository repository, ILogger<InterviewService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Interview model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Interview model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.InterviewDate = model.InterviewDate;
            existing.Feedback = model.Feedback;
            existing.Stage = model.Stage;
            existing.Result = model.Result;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Interview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Interview>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToInterviewers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInterviewers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
