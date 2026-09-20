using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IBonusPlanRepository
{
    Task<BonusPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BonusPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BonusPlan bonusPlan, CancellationToken cancellationToken);
    Task UpdateAsync(BonusPlan bonusPlan, CancellationToken cancellationToken);
    Task DeleteAsync(BonusPlan bonusPlan, CancellationToken cancellationToken);
}
