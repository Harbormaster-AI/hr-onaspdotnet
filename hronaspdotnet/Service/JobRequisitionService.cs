
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface IJobRequisitionService {

    Task Create(JobRequisition model , CancellationToken cancellationToken);
    Task<bool> Update(JobRequisition model, CancellationToken cancellationToken);
    Task<JobRequisition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<JobRequisition>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignHiringManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignHiringManager(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRecruiter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRecruiter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class JobRequisitionService : IJobRequisitionService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IJobRequisitionRepository _repository;
    private readonly ILogger<JobRequisitionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public JobRequisitionService(
        ApplicationTelemetry telemetry,
        IJobRequisitionRepository repository,
        ILogger<JobRequisitionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(JobRequisition model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "JobRequisition",
                "CreateJobRequisition",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(JobRequisition model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RequisitionNumber = model.RequisitionNumber;
            existing.Title = model.Title;
            existing.Openings = model.Openings;
            existing.TargetStartDate = model.TargetStartDate;
            existing.Status = model.Status;
            existing.Priority = model.Priority;

            await _telemetry.Execute(
                "JobRequisition",
                "UpdateJobRequisition",
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

    public Task<JobRequisition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<JobRequisition>> GetAll(CancellationToken cancellationToken)
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
                "JobRequisition",
                "UpdateJobRequisition",
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

    public async Task<bool> AssignDepartment(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<DepartmentService>().Get(childRequest, cancellationToken);
            parent.Department = child;
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

    public async Task<bool> UnassignDepartment(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Department = null;
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

    public async Task<bool> AssignHiringManager(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EmployeeService>().Get(childRequest, cancellationToken);
            parent.HiringManager = child;
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

    public async Task<bool> UnassignHiringManager(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.HiringManager = null;
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

    public async Task<bool> AssignRecruiter(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<EmployeeService>().Get(childRequest, cancellationToken);
            parent.Recruiter = child;
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

    public async Task<bool> UnassignRecruiter(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Recruiter = null;
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

    public async Task<bool> AssignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<JobProfileService>().Get(childRequest, cancellationToken);
            parent.JobProfile = child;
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

    public async Task<bool> UnassignJobProfile(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No JobRequisition found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.JobProfile = null;
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


    public async Task<bool> AddToCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "AddToCandidates",
                () => _repository.AddToCandidatesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCandidates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "RemoveFromCandidates",
                () => _repository.RemoveFromCandidatesAsync(request, cancellationToken));
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

    public async Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "AddToInterviews",
                () => _repository.AddToInterviewsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "RemoveFromInterviews",
                () => _repository.RemoveFromInterviewsAsync(request, cancellationToken));
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

    public async Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "AddToOffers",
                () => _repository.AddToOffersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "JobRequisition",
                "RemoveFromOffers",
                () => _repository.RemoveFromOffersAsync(request, cancellationToken));
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
