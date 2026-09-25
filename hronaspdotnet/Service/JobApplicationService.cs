
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface IJobApplicationService {

    Task Create(JobApplication model , CancellationToken cancellationToken);
    Task<bool> Update(JobApplication model, CancellationToken cancellationToken);
    Task<JobApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobApplication>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobApplicationService : IJobApplicationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IJobApplicationRepository _repository;
    private readonly ILogger<JobApplicationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public JobApplicationService(
        ApplicationTelemetry telemetry,
        IJobApplicationRepository repository,
        ILogger<JobApplicationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(JobApplication model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "JobApplication",
                "CreateJobApplication",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(JobApplication model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ApplicationNumber = model.ApplicationNumber;
            existing.AppliedDate = model.AppliedDate;
            existing.ResumeUrl = model.ResumeUrl;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "JobApplication",
                "UpdateJobApplication",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<JobApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobApplication>> GetAll(CancellationToken cancellationToken)
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
            await _telemetry.Execute(
                "JobApplication",
                "UpdateJobApplication",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignCandidate(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CandidateService>().Get(childRequest, cancellationToken);
            parent.Candidate = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignCandidate(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Candidate = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignRequisition(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<JobRequisitionService>().Get(childRequest, cancellationToken);
            parent.Requisition = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignRequisition(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobApplication found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Requisition = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobApplication",
                "AddToScreenings",
                () => _repository.AddToScreeningsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromScreenings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobApplication",
                "RemoveFromScreenings",
                () => _repository.RemoveFromScreeningsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
