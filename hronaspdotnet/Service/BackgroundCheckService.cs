using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IBackgroundCheckService {

    Task Create(BackgroundCheck model , CancellationToken cancellationToken);
    Task<bool> Update(BackgroundCheck model, CancellationToken cancellationToken);
    Task<BackgroundCheck?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BackgroundCheck>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignReport(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignReport(AssociationRequest request, CancellationToken cancellationToken);


}

public class BackgroundCheckService : IBackgroundCheckService
{
    private readonly IBackgroundCheckRepository _repository;
    private readonly ILogger<BackgroundCheckService> _logger;

    public BackgroundCheckService(
        IBackgroundCheckRepository repository, ILogger<BackgroundCheckService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(BackgroundCheck model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(BackgroundCheck model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.CheckNumber = model.CheckNumber;
            existing.Provider = model.Provider;
            existing.CompletedDate = model.CompletedDate;
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

    public Task<BackgroundCheck?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BackgroundCheck>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignReport(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignReport(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
