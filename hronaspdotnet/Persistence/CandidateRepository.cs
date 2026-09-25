
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CandidateRepository : ICandidateRepository
{
    private readonly ApplicationDbContext _db;

    public CandidateRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Candidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Candidates
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Candidate>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Candidates
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        _db.Candidates.Add(candidate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        _db.Candidates.Update(candidate);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        _db.Candidates.Remove(candidate);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobApplications
            .Where(jobApplication =>
                request.ChildIds.Contains(jobApplication.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobApplication =>
                        EF.Property<Guid?>(
                            jobApplication,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.JobApplications
            .Where(jobApplication =>
                request.ChildIds.Contains(jobApplication.Id) &&
                EF.Property<Guid?>(
                    jobApplication,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    jobApplication =>
                        EF.Property<Guid?>(
                            jobApplication,
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


    public async Task AddToDocumentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Documents
            .Where(document =>
                request.ChildIds.Contains(document.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    document =>
                        EF.Property<Guid?>(
                            document,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDocumentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Documents
            .Where(document =>
                request.ChildIds.Contains(document.Id) &&
                EF.Property<Guid?>(
                    document,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    document =>
                        EF.Property<Guid?>(
                            document,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
