using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Department department, CancellationToken cancellationToken);
    Task UpdateAsync(Department department, CancellationToken cancellationToken);
    Task DeleteAsync(Department department, CancellationToken cancellationToken);

    Task AddToPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
