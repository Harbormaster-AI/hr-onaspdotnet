using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class OnboardingTaskRepository : IOnboardingTaskRepository
{
    private readonly ApplicationDbContext _db;

    public OnboardingTaskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<OnboardingTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.OnboardingTasks
            .Include(x => x.Employee)
            .Include(x => x.AssignedTo)
            .Include(x => x.RelatedOffer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<OnboardingTask>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.OnboardingTasks
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.AssignedTo)
            .Include(x => x.RelatedOffer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken)
    {
        _db.OnboardingTasks.Add(onboardingTask);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken)
    {
        _db.OnboardingTasks.Update(onboardingTask);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken)
    {
        _db.OnboardingTasks.Remove(onboardingTask);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
