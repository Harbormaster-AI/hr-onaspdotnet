using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class JobFamilyRepository : IJobFamilyRepository
{
    private readonly ApplicationDbContext _db;

    public JobFamilyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<JobFamily?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.JobFamilys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<JobFamily>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.JobFamilys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JobFamily jobFamily, CancellationToken cancellationToken)
    {
        _db.JobFamilys.Add(jobFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JobFamily jobFamily, CancellationToken cancellationToken)
    {
        _db.JobFamilys.Update(jobFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JobFamily jobFamily, CancellationToken cancellationToken)
    {
        _db.JobFamilys.Remove(jobFamily);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
