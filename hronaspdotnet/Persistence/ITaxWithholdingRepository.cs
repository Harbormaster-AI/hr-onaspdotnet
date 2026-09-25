using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ITaxWithholdingRepository
{
    Task<TaxWithholding?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TaxWithholding>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken);
    Task UpdateAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken);
    Task DeleteAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken);


}
