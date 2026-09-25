
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TerminationRepository : ITerminationRepository
{
    private readonly ApplicationDbContext _db;

    public TerminationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Termination?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Terminations
            .Include(x => x.Employee)
            .Include(x => x.Assignment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Termination>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Terminations
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Assignment)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Termination termination, CancellationToken cancellationToken)
    {
        _db.Terminations.Add(termination);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Termination termination, CancellationToken cancellationToken)
    {
        _db.Terminations.Update(termination);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Termination termination, CancellationToken cancellationToken)
    {
        _db.Terminations.Remove(termination);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
