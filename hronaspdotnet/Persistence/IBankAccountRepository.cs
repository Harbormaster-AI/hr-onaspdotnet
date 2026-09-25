using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IBankAccountRepository
{
    Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BankAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BankAccount bankAccount, CancellationToken cancellationToken);
    Task UpdateAsync(BankAccount bankAccount, CancellationToken cancellationToken);
    Task DeleteAsync(BankAccount bankAccount, CancellationToken cancellationToken);


}
