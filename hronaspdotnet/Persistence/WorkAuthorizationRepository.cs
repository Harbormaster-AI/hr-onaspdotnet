
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class WorkAuthorizationRepository : IWorkAuthorizationRepository
{
    private readonly ApplicationDbContext _db;

    public WorkAuthorizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<WorkAuthorization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.WorkAuthorizations
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<WorkAuthorization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.WorkAuthorizations
            .AsNoTracking()
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken)
    {
        _db.WorkAuthorizations.Add(workAuthorization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken)
    {
        _db.WorkAuthorizations.Update(workAuthorization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(WorkAuthorization workAuthorization, CancellationToken cancellationToken)
    {
        _db.WorkAuthorizations.Remove(workAuthorization);
        await _db.SaveChangesAsync(cancellationToken);
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
