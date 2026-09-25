using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface ICompensationPackageRepository
{
    Task<CompensationPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompensationPackage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);
    Task UpdateAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);
    Task DeleteAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken);

    Task AddToSalaryComponentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSalaryComponentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBonusPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBonusPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEquityGrantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEquityGrantsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
