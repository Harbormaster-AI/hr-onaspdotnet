using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IGoalRepository
{
    Task<Goal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Goal>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Goal goal, CancellationToken cancellationToken);
    Task UpdateAsync(Goal goal, CancellationToken cancellationToken);
    Task DeleteAsync(Goal goal, CancellationToken cancellationToken);

    Task AddToChildGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChildGoalsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
