using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class BenefitPlanRepository : IBenefitPlanRepository
{
    private readonly ApplicationDbContext _db;

    public BenefitPlanRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BenefitPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BenefitPlans
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BenefitPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BenefitPlans
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken)
    {
        _db.BenefitPlans.Add(benefitPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken)
    {
        _db.BenefitPlans.Update(benefitPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BenefitPlan benefitPlan, CancellationToken cancellationToken)
    {
        _db.BenefitPlans.Remove(benefitPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
