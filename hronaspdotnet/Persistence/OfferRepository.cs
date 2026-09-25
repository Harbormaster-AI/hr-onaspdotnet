
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class OfferRepository : IOfferRepository
{
    private readonly ApplicationDbContext _db;

    public OfferRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Offer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Offers
            .Include(x => x.Requisition)
            .Include(x => x.Candidate)
            .Include(x => x.ApprovedBy)
            .Include(x => x.Contract)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Offer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Offers
            .AsNoTracking()
            .Include(x => x.Requisition)
            .Include(x => x.Candidate)
            .Include(x => x.ApprovedBy)
            .Include(x => x.Contract)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Offer offer, CancellationToken cancellationToken)
    {
        _db.Offers.Add(offer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Offer offer, CancellationToken cancellationToken)
    {
        _db.Offers.Update(offer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Offer offer, CancellationToken cancellationToken)
    {
        _db.Offers.Remove(offer);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
