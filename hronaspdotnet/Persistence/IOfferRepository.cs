using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IOfferRepository
{
    Task<Offer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Offer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Offer offer, CancellationToken cancellationToken);
    Task UpdateAsync(Offer offer, CancellationToken cancellationToken);
    Task DeleteAsync(Offer offer, CancellationToken cancellationToken);


}
