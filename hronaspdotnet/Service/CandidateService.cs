using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface ICandidateService {

    Task Create(Candidate model , CancellationToken cancellationToken);
    Task<bool> Update(Candidate model, CancellationToken cancellationToken);
    Task<Candidate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Candidate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repository;
    private readonly ILogger<CandidateService> _logger;

    public CandidateService(
        ICandidateRepository repository, ILogger<CandidateService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Candidate model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Candidate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Source = model.Source;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Candidate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Candidate>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
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

    public async Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
