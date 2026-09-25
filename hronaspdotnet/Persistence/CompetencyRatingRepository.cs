
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class CompetencyRatingRepository : ICompetencyRatingRepository
{
    private readonly ApplicationDbContext _db;

    public CompetencyRatingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CompetencyRating?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CompetencyRatings
            .Include(x => x.Review)
            .Include(x => x.Competency)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CompetencyRating>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CompetencyRatings
            .AsNoTracking()
            .Include(x => x.Review)
            .Include(x => x.Competency)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CompetencyRating competencyRating, CancellationToken cancellationToken)
    {
        _db.CompetencyRatings.Add(competencyRating);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CompetencyRating competencyRating, CancellationToken cancellationToken)
    {
        _db.CompetencyRatings.Update(competencyRating);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CompetencyRating competencyRating, CancellationToken cancellationToken)
    {
        _db.CompetencyRatings.Remove(competencyRating);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
