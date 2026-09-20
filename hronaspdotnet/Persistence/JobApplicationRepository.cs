using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public JobApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<JobApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.JobApplications
            .Include(x => x.Candidate)
            .Include(x => x.Requisition)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<JobApplication>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.JobApplications
            .AsNoTracking()
            .Include(x => x.Candidate)
            .Include(x => x.Requisition)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JobApplication jobApplication, CancellationToken cancellationToken)
    {
        _db.JobApplications.Add(jobApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JobApplication jobApplication, CancellationToken cancellationToken)
    {
        _db.JobApplications.Update(jobApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JobApplication jobApplication, CancellationToken cancellationToken)
    {
        _db.JobApplications.Remove(jobApplication);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
