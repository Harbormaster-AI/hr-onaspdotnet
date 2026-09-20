using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IJobProfileService {

    Task Create(JobProfile model , CancellationToken cancellationToken);
    Task<bool> Update(JobProfile model, CancellationToken cancellationToken);
    Task<JobProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobProfile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignJobFamily(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignJobFamily(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCompetencies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompetencies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTrainingRecommendations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrainingRecommendations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobProfileService : IJobProfileService
{
    private readonly IJobProfileRepository _repository;
    private readonly ILogger<JobProfileService> _logger;

    public JobProfileService(
        IJobProfileRepository repository, ILogger<JobProfileService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(JobProfile model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(JobProfile model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.JobCode = model.JobCode;
            existing.JobLevel = model.JobLevel;
            existing.ExemptStatus = model.ExemptStatus;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<JobProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobProfile>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignJobFamily(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignJobFamily(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCompetencies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompetencies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTrainingRecommendations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrainingRecommendations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPositions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
