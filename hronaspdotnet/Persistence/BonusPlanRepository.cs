
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class BonusPlanRepository : IBonusPlanRepository
{
    private readonly ApplicationDbContext _db;

    public BonusPlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BonusPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BonusPlans
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BonusPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BonusPlans
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BonusPlan bonusPlan, CancellationToken cancellationToken)
    {
        _db.BonusPlans.Add(bonusPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BonusPlan bonusPlan, CancellationToken cancellationToken)
    {
        _db.BonusPlans.Update(bonusPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BonusPlan bonusPlan, CancellationToken cancellationToken)
    {
        _db.BonusPlans.Remove(bonusPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCompensationPackagesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompensationPackages
            .Where(compensationPackage =>
                request.ChildIds.Contains(compensationPackage.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    compensationPackage =>
                        EF.Property<Guid?>(
                            compensationPackage,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCompensationPackagesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompensationPackages
            .Where(compensationPackage =>
                request.ChildIds.Contains(compensationPackage.Id) &&
                EF.Property<Guid?>(
                    compensationPackage,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    compensationPackage =>
                        EF.Property<Guid?>(
                            compensationPackage,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
