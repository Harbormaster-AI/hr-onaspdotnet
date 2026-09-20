using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ICostCenterRepository
{
    Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CostCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken);
    Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken);
    Task DeleteAsync(CostCenter costCenter, CancellationToken cancellationToken);
}
