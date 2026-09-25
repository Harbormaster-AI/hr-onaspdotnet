
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CompensationPackageRepository : ICompensationPackageRepository
{
    private readonly ApplicationDbContext _db;

    public CompensationPackageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CompensationPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CompensationPackages
            .Include(x => x.Contract)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CompensationPackage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CompensationPackages
            .AsNoTracking()
            .Include(x => x.Contract)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken)
    {
        _db.CompensationPackages.Add(compensationPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken)
    {
        _db.CompensationPackages.Update(compensationPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CompensationPackage compensationPackage, CancellationToken cancellationToken)
    {
        _db.CompensationPackages.Remove(compensationPackage);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToSalaryComponentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalaryComponents
            .Where(salaryComponent =>
                request.ChildIds.Contains(salaryComponent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salaryComponent =>
                        EF.Property<Guid?>(
                            salaryComponent,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSalaryComponentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SalaryComponents
            .Where(salaryComponent =>
                request.ChildIds.Contains(salaryComponent.Id) &&
                EF.Property<Guid?>(
                    salaryComponent,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    salaryComponent =>
                        EF.Property<Guid?>(
                            salaryComponent,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToBonusPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BonusPlans
            .Where(bonusPlan =>
                request.ChildIds.Contains(bonusPlan.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bonusPlan =>
                        EF.Property<Guid?>(
                            bonusPlan,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBonusPlansAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BonusPlans
            .Where(bonusPlan =>
                request.ChildIds.Contains(bonusPlan.Id) &&
                EF.Property<Guid?>(
                    bonusPlan,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    bonusPlan =>
                        EF.Property<Guid?>(
                            bonusPlan,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToEquityGrantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EquityGrants
            .Where(equityGrant =>
                request.ChildIds.Contains(equityGrant.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    equityGrant =>
                        EF.Property<Guid?>(
                            equityGrant,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEquityGrantsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.EquityGrants
            .Where(equityGrant =>
                request.ChildIds.Contains(equityGrant.Id) &&
                EF.Property<Guid?>(
                    equityGrant,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    equityGrant =>
                        EF.Property<Guid?>(
                            equityGrant,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
