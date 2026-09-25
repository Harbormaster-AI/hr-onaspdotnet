
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CertificationRepository : ICertificationRepository
{
    private readonly ApplicationDbContext _db;

    public CertificationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Certification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Certifications
            .Include(x => x.Employee)
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Certification>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Certifications
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Course)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Certification certification, CancellationToken cancellationToken)
    {
        _db.Certifications.Add(certification);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Certification certification, CancellationToken cancellationToken)
    {
        _db.Certifications.Update(certification);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Certification certification, CancellationToken cancellationToken)
    {
        _db.Certifications.Remove(certification);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
