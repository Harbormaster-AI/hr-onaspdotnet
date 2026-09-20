using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class JobProfileRepository : IJobProfileRepository
{
    private readonly ApplicationDbContext _db;

    public JobProfileRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<JobProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.JobProfiles
            .Include(x => x.JobFamily)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<JobProfile>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.JobProfiles
            .AsNoTracking()
            .Include(x => x.JobFamily)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Add(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Update(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JobProfile jobProfile, CancellationToken cancellationToken)
    {
        _db.JobProfiles.Remove(jobProfile);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
