using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class BenefitEnrollmentRepository : IBenefitEnrollmentRepository
{
    private readonly ApplicationDbContext _db;

    public BenefitEnrollmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BenefitEnrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BenefitEnrollments
            .Include(x => x.BenefitPlan)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BenefitEnrollment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BenefitEnrollments
            .AsNoTracking()
            .Include(x => x.BenefitPlan)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken)
    {
        _db.BenefitEnrollments.Add(benefitEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken)
    {
        _db.BenefitEnrollments.Update(benefitEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BenefitEnrollment benefitEnrollment, CancellationToken cancellationToken)
    {
        _db.BenefitEnrollments.Remove(benefitEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
