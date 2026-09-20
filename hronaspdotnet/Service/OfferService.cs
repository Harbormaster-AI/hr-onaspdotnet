using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IOfferService {

    Task Create(Offer model , CancellationToken cancellationToken);
    Task<bool> Update(Offer model, CancellationToken cancellationToken);
    Task<Offer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Offer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignApprovedBy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignApprovedBy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken);


}

public class OfferService : IOfferService
{
    private readonly IOfferRepository _repository;
    private readonly ILogger<OfferService> _logger;

    public OfferService(
        IOfferRepository repository, ILogger<OfferService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Offer model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Offer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.OfferNumber = model.OfferNumber;
            existing.ProposedStartDate = model.ProposedStartDate;
            existing.BaseSalary = model.BaseSalary;
            existing.SignOnBonus = model.SignOnBonus;
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

    public Task<Offer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Offer>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignApprovedBy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignApprovedBy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignContract(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignContract(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
