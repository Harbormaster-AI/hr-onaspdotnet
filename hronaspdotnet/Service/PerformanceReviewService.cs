using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPerformanceReviewService {

    Task Create(PerformanceReview model , CancellationToken cancellationToken);
    Task<bool> Update(PerformanceReview model, CancellationToken cancellationToken);
    Task<PerformanceReview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PerformanceReview>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignReviewer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignReviewer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCycle(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCycle(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCompetencyRatings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompetencyRatings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGoals(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PerformanceReviewService : IPerformanceReviewService
{
    private readonly IPerformanceReviewRepository _repository;
    private readonly ILogger<PerformanceReviewService> _logger;

    public PerformanceReviewService(
        IPerformanceReviewRepository repository, ILogger<PerformanceReviewService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PerformanceReview model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PerformanceReview model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReviewNumber = model.ReviewNumber;
            existing.ReviewDate = model.ReviewDate;
            existing.ReviewerComments = model.ReviewerComments;
            existing.Rating = model.Rating;
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

    public Task<PerformanceReview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PerformanceReview>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignReviewer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignReviewer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCycle(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCycle(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCompetencyRatings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompetencyRatings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGoals(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
