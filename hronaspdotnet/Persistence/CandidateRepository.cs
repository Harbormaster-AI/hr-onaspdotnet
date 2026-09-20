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
}
