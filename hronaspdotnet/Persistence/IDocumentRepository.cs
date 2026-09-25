using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Document>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Document document, CancellationToken cancellationToken);
    Task UpdateAsync(Document document, CancellationToken cancellationToken);
    Task DeleteAsync(Document document, CancellationToken cancellationToken);


}
