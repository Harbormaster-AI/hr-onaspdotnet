
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class EmploymentAssignmentRepository : IEmploymentAssignmentRepository
{
    private readonly ApplicationDbContext _db;

    public EmploymentAssignmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EmploymentAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.EmploymentAssignments
            .Include(x => x.Employee)
            .Include(x => x.Position)
            .Include(x => x.Supervisor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EmploymentAssignment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.EmploymentAssignments
            .AsNoTracking()
            .Include(x => x.Employee)
            .Include(x => x.Position)
            .Include(x => x.Supervisor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken)
    {
        _db.EmploymentAssignments.Add(employmentAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken)
    {
        _db.EmploymentAssignments.Update(employmentAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmploymentAssignment employmentAssignment, CancellationToken cancellationToken)
    {
        _db.EmploymentAssignments.Remove(employmentAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
