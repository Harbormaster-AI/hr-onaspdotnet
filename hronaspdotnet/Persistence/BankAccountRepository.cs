
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _db;

    public BankAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BankAccounts
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BankAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BankAccounts
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        _db.BankAccounts.Add(bankAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        _db.BankAccounts.Update(bankAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BankAccount bankAccount, CancellationToken cancellationToken)
    {
        _db.BankAccounts.Remove(bankAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
