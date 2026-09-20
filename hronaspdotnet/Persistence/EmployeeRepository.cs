using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _db;

    public EmployeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Employees
            .Include(x => x.Manager)
            .Include(x => x.Department)
            .Include(x => x.PrimaryLocation)
            .Include(x => x.CostCenter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Employees
            .AsNoTracking()
            .Include(x => x.Manager)
            .Include(x => x.Department)
            .Include(x => x.PrimaryLocation)
            .Include(x => x.CostCenter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Update(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
