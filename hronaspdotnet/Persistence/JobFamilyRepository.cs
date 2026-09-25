
using hronaspdotnet.Contracts;
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


    public async Task AddToJobProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobProfiles
            .Where(jobProfile =>
                request.ChildIds.Contains(jobProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobProfile =>
                        EF.Property<Guid?>(
                            jobProfile,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromJobProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobProfiles
            .Where(jobProfile =>
                request.ChildIds.Contains(jobProfile.Id) &&
                EF.Property<Guid?>(
                    jobProfile,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobProfile =>
                        EF.Property<Guid?>(
                            jobProfile,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
