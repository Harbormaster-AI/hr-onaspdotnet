
using hronaspdotnet.Domain;
using hronaspdotnet.Persistence;
using hronaspdotnet.Contracts;
using hronaspdotnet.Telemetry;

namespace hronaspdotnet.Service;

public interface ICandidateService {

    Task Create(Candidate model , CancellationToken cancellationToken);
    Task<bool> Update(Candidate model, CancellationToken cancellationToken);
    Task<Candidate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Candidate>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInterviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOffers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CandidateService : ICandidateService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICandidateRepository _repository;
    private readonly ILogger<CandidateService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CandidateService(
        ApplicationTelemetry telemetry,
        ICandidateRepository repository,
        ILogger<CandidateService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Candidate model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Candidate",
                "CreateCandidate",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Candidate model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Source = model.Source;

            await _telemetry.Execute(
                "Candidate",
                "UpdateCandidate",
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

    public Task<Candidate?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Candidate>> GetAll(CancellationToken cancellationToken)
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
                "Candidate",
                "UpdateCandidate",
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


    public async Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Candidate",
                "AddToApplications",
                () => _repository.AddToApplicationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Candidate",
                "RemoveFromApplications",
                () => _repository.RemoveFromApplicationsAsync(request, cancellationToken));
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
                "Candidate",
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
                "Candidate",
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
                "Candidate",
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
                "Candidate",
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

    public async Task<bool> AddToDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Candidate",
                "AddToDocuments",
                () => _repository.AddToDocumentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDocuments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Candidate",
                "RemoveFromDocuments",
                () => _repository.RemoveFromDocumentsAsync(request, cancellationToken));
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
