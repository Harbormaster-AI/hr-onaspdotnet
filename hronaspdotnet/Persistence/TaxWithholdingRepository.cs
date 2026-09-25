
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TaxWithholdingRepository : ITaxWithholdingRepository
{
    private readonly ApplicationDbContext _db;

    public TaxWithholdingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TaxWithholding?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TaxWithholdings
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TaxWithholding>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TaxWithholdings
            .AsNoTracking()
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken)
    {
        _db.TaxWithholdings.Add(taxWithholding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken)
    {
        _db.TaxWithholdings.Update(taxWithholding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaxWithholding taxWithholding, CancellationToken cancellationToken)
    {
        _db.TaxWithholdings.Remove(taxWithholding);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
