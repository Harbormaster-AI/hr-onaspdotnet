using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ICompensationPackageRepository
{
    Task<CompensationPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompensationPackage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);
    Task UpdateAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);
    Task DeleteAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);
}
