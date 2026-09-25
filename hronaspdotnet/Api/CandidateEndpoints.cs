
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CandidateEndpoints
{
    public static IEndpointRouteBuilder MapCandidateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/candidate").WithTags("Candidates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToApplications", AddToApplications);
    group.MapPut("/removeFromApplications", RemoveFromApplications);

    group.MapPut("/addToInterviews", AddToInterviews);
    group.MapPut("/removeFromInterviews", RemoveFromInterviews);

    group.MapPut("/addToOffers", AddToOffers);
    group.MapPut("/removeFromOffers", RemoveFromOffers);

    group.MapPut("/addToDocuments", AddToDocuments);
    group.MapPut("/removeFromDocuments", RemoveFromDocuments);


        return app;
    }

    private static async Task<IResult> Create(
        CandidateRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCandidate( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        CandidateRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCandidate( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ICandidateService service,
        CancellationToken cancellationToken) {

        var candidate = await service.Get(identifier, cancellationToken);
        return candidate is null ? Results.NotFound() : Results.Ok( candidate );
    }


    private static async Task<IResult> GetAll(
        ICandidateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CandidateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToApplications(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApplications(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApplications(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApplications(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInterviews(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInterviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInterviews(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInterviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOffers(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOffers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOffers(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOffers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDocuments(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDocuments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDocuments(
        MultipleAssociationRequest request,
        ICandidateService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDocuments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Candidate mapRequestToCandidate( CandidateRequest request ) {
        var model = new Candidate
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Source = request.Source,
        };
        return model;
    }

}
