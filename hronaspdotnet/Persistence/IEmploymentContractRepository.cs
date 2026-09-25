using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IEmploymentContractRepository
{
    Task<EmploymentContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EmploymentContract>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(EmploymentContract employmentContract, CancellationToken cancellationToken);
    Task UpdateAsync(EmploymentContract employmentContract, CancellationToken cancellationToken);
    Task DeleteAsync(EmploymentContract employmentContract, CancellationToken cancellationToken);


}
