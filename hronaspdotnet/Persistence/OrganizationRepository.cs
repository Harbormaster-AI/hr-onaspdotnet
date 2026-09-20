using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _db;

    public OrganizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Add(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Update(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Remove(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
