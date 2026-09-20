using hronaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace hronaspdotnet.Persistence;

public class TrainingCourseRepository : ITrainingCourseRepository
{
    private readonly ApplicationDbContext _db;

    public TrainingCourseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TrainingCourse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TrainingCourses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TrainingCourse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TrainingCourses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Add(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Update(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TrainingCourse trainingCourse, CancellationToken cancellationToken)
    {
        _db.TrainingCourses.Remove(trainingCourse);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
