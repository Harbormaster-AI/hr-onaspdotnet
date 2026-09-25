using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICostCenterRepository
{
    Task<CostCenter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CostCenter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken);
    Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken);
    Task DeleteAsync(CostCenter costCenter, CancellationToken cancellationToken);

    Task AddToDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
