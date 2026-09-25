
using hronaspdotnet.Contracts;
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


    public async Task AddToCandidatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Candidates
            .Where(candidate =>
                request.ChildIds.Contains(candidate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    candidate =>
                        EF.Property<Guid?>(
                            candidate,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCandidatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Candidates
            .Where(candidate =>
                request.ChildIds.Contains(candidate.Id) &&
                EF.Property<Guid?>(
                    candidate,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    candidate =>
                        EF.Property<Guid?>(
                            candidate,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToInterviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Interviews
            .Where(interview =>
                request.ChildIds.Contains(interview.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    interview =>
                        EF.Property<Guid?>(
                            interview,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInterviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Interviews
            .Where(interview =>
                request.ChildIds.Contains(interview.Id) &&
                EF.Property<Guid?>(
                    interview,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    interview =>
                        EF.Property<Guid?>(
                            interview,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }


    public async Task AddToOffersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Offers
            .Where(offer =>
                request.ChildIds.Contains(offer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    offer =>
                        EF.Property<Guid?>(
                            offer,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOffersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Offers
            .Where(offer =>
                request.ChildIds.Contains(offer.Id) &&
                EF.Property<Guid?>(
                    offer,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    offer =>
                        EF.Property<Guid?>(
                            offer,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
