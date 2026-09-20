using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class JobRequisitionRepository : IJobRequisitionRepository
{
    private readonly ApplicationDbContext _db;

    public JobRequisitionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<JobRequisition?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.JobRequisitions
            .Include(x => x.Department)
            .Include(x => x.HiringManager)
            .Include(x => x.Recruiter)
            .Include(x => x.JobProfile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<JobRequisition>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.JobRequisitions
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.HiringManager)
            .Include(x => x.Recruiter)
            .Include(x => x.JobProfile)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JobRequisition jobRequisition, CancellationToken cancellationToken)
    {
        _db.JobRequisitions.Add(jobRequisition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JobRequisition jobRequisition, CancellationToken cancellationToken)
    {
        _db.JobRequisitions.Update(jobRequisition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JobRequisition jobRequisition, CancellationToken cancellationToken)
    {
        _db.JobRequisitions.Remove(jobRequisition);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
