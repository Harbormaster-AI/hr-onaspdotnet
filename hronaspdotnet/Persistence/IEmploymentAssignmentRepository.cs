using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Persistence;

public interface IEmploymentAssignmentRepository
{
    Task<EmploymentAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EmploymentAssignment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken);
    Task UpdateAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken);
    Task DeleteAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken);


}
