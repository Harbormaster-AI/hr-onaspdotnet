
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface IBankAccountService {

    Task Create(BankAccount model , CancellationToken cancellationToken);
    Task<bool> Update(BankAccount model, CancellationToken cancellationToken);
    Task<BankAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<BankAccount>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------


}

public class BankAccountService : IBankAccountService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IBankAccountRepository _repository;
    private readonly ILogger<BankAccountService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public BankAccountService(
        ApplicationTelemetry telemetry,
        IBankAccountRepository repository,
        ILogger<BankAccountService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(BankAccount model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "BankAccount",
                "CreateBankAccount",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(BankAccount model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AccountHolder = model.AccountHolder;
            existing.BankName = model.BankName;
            existing.Iban = model.Iban;
            existing.Bic = model.Bic;
            existing.AccountNumber = model.AccountNumber;
            existing.RoutingNumber = model.RoutingNumber;

            await _telemetry.Execute(
                "BankAccount",
                "UpdateBankAccount",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<BankAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<BankAccount>> GetAll(CancellationToken cancellationToken)
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
            await _telemetry.Execute(
                "BankAccount",
                "UpdateBankAccount",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
