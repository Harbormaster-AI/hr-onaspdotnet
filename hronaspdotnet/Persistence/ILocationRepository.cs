using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ILocationRepository
{
    Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Location>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Location location, CancellationToken cancellationToken);
    Task UpdateAsync(Location location, CancellationToken cancellationToken);
    Task DeleteAsync(Location location, CancellationToken cancellationToken);

    Task AddToDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDepartmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPositionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEmployeesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
