using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public interface IOnboardingTaskRepository
{
    Task<OnboardingTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<OnboardingTask>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken);
    Task UpdateAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken);
    Task DeleteAsync(OnboardingTask onboardingTask, CancellationToken cancellationToken);
}
