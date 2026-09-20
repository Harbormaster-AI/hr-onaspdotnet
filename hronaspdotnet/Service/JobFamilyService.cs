using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Service;

public interface IJobFamilyService {

    Task Create(JobFamily model , CancellationToken cancellationToken);
    Task<bool> Update(JobFamily model, CancellationToken cancellationToken);
    Task<JobFamily?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobFamily>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobFamilyService : IJobFamilyService
{
    private readonly IJobFamilyRepository _repository;
    private readonly ILogger<JobFamilyService> _logger;

    public JobFamilyService(
        IJobFamilyRepository repository, ILogger<JobFamilyService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(JobFamily model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(JobFamily model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Description = model.Description;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<JobFamily?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobFamily>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromJobProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
