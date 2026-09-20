using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Department department, CancellationToken cancellationToken);
    Task UpdateAsync(Department department, CancellationToken cancellationToken);
    Task DeleteAsync(Department department, CancellationToken cancellationToken);
}
