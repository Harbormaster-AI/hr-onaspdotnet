using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IPaymentMethodService {

    Task Create(PaymentMethod model , CancellationToken cancellationToken);
    Task<bool> Update(PaymentMethod model, CancellationToken cancellationToken);
    Task<PaymentMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentMethod>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEmployee(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignBankAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBankAccount(AssociationRequest request, CancellationToken cancellationToken);


}

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _repository;
    private readonly ILogger<PaymentMethodService> _logger;

    public PaymentMethodService(
        IPaymentMethodRepository repository, ILogger<PaymentMethodService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PaymentMethod model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PaymentMethod model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Preferred = model.Preferred;
            existing.MethodType = model.MethodType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PaymentMethod?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PaymentMethod>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBankAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBankAccount(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
