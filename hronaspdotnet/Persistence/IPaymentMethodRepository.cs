using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken);
    Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken);
    Task DeleteAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken);


}
