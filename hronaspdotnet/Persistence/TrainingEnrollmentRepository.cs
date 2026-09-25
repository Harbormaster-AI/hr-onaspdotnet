
using hronaspdotnet.Contracts;
using hronaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TrainingEnrollmentRepository : ITrainingEnrollmentRepository
{
    private readonly ApplicationDbContext _db;

    public TrainingEnrollmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TrainingEnrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TrainingEnrollments
            .Include(x => x.Course)
            .Include(x => x.Employee)
            .Include(x => x.Instructor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TrainingEnrollment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TrainingEnrollments
            .AsNoTracking()
            .Include(x => x.Course)
            .Include(x => x.Employee)
            .Include(x => x.Instructor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken)
    {
        _db.TrainingEnrollments.Add(trainingEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken)
    {
        _db.TrainingEnrollments.Update(trainingEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TrainingEnrollment trainingEnrollment, CancellationToken cancellationToken)
    {
        _db.TrainingEnrollments.Remove(trainingEnrollment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
