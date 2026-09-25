
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class InterviewRepository : IInterviewRepository
{
    private readonly ApplicationDbContext _db;

    public InterviewRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Interview?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Interviews
            .Include(x => x.Requisition)
            .Include(x => x.Candidate)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Interview>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Interviews
            .AsNoTracking()
            .Include(x => x.Requisition)
            .Include(x => x.Candidate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Interview interview, CancellationToken cancellationToken)
    {
        _db.Interviews.Add(interview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Interview interview, CancellationToken cancellationToken)
    {
        _db.Interviews.Update(interview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Interview interview, CancellationToken cancellationToken)
    {
        _db.Interviews.Remove(interview);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToInterviewersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Employees
            .Where(employee =>
                request.ChildIds.Contains(employee.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employee =>
                        EF.Property<Guid?>(
                            employee,
                            "WorkAuthorization_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInterviewersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Employees
            .Where(employee =>
                request.ChildIds.Contains(employee.Id) &&
                EF.Property<Guid?>(
                    employee,
                    "WorkAuthorization_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    employee =>
                        EF.Property<Guid?>(
                            employee,
                            "WorkAuthorization_Id"),
                    (Guid?)null));
    }

}
