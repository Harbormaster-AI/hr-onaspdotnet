using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _db;

    public DepartmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Departments
            .Include(x => x.Organization)
            .Include(x => x.Manager)
            .Include(x => x.CostCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Departments
            .AsNoTracking()
            .Include(x => x.Organization)
            .Include(x => x.Manager)
            .Include(x => x.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Add(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Update(department);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Department department, CancellationToken cancellationToken)
    {
        _db.Departments.Remove(department);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
