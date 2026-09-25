
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ApplicationDbContext _db;

    public ScreeningRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Screening?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Screenings
            .Include(x => x.Application)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Screening>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Screenings
            .AsNoTracking()
            .Include(x => x.Application)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Add(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Update(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Screening screening, CancellationToken cancellationToken)
    {
        _db.Screenings.Remove(screening);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
