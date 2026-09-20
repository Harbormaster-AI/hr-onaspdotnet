using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ICompetencyRatingService {

    Task Create(CompetencyRating model , CancellationToken cancellationToken);
    Task<bool> Update(CompetencyRating model, CancellationToken cancellationToken);
    Task<CompetencyRating?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompetencyRating>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignReview(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignReview(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCompetency(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCompetency(AssociationRequest request, CancellationToken cancellationToken);


}

public class CompetencyRatingService : ICompetencyRatingService
{
    private readonly ICompetencyRatingRepository _repository;
    private readonly ILogger<CompetencyRatingService> _logger;

    public CompetencyRatingService(
        ICompetencyRatingRepository repository, ILogger<CompetencyRatingService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(CompetencyRating model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(CompetencyRating model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Comment = model.Comment;
            existing.Rating = model.Rating;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<CompetencyRating?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CompetencyRating>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignReview(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignReview(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCompetency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCompetency(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
