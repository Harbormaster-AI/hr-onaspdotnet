using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface ISalaryComponentRepository
{
    Task<SalaryComponent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SalaryComponent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken);
    Task UpdateAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken);
    Task DeleteAsync(SalaryComponent salaryComponent, CancellationToken cancellationToken);
}
