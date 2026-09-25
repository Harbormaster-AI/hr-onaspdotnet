using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPositionRepository
{
    Task<Position?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Position>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Position position, CancellationToken cancellationToken);
    Task UpdateAsync(Position position, CancellationToken cancellationToken);
    Task DeleteAsync(Position position, CancellationToken cancellationToken);

    Task AddToDirectReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDirectReportsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
