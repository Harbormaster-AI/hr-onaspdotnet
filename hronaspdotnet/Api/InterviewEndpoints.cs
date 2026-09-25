
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class InterviewEndpoints
{
    public static IEndpointRouteBuilder MapInterviewEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/interview").WithTags("Interviews");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRequisition", AssignRequisition);
        group.MapPut("/unassignRequisition", UnassignRequisition);
        group.MapPut("/assignCandidate", AssignCandidate);
        group.MapPut("/unassignCandidate", UnassignCandidate);

    group.MapPut("/addToInterviewers", AddToInterviewers);
    group.MapPut("/removeFromInterviewers", RemoveFromInterviewers);


        return app;
    }

    private static async Task<IResult> Create(
        InterviewRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInterview( request );

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
        InterviewRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInterview( request );

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
        IInterviewService service,
        CancellationToken cancellationToken) {

        var interview = await service.Get(identifier, cancellationToken);
        return interview is null ? Results.NotFound() : Results.Ok( interview );
    }


    private static async Task<IResult> GetAll(
        IInterviewService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InterviewResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInterviewService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRequisition(
        AssociationRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRequisition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRequisition(
    AssociationRequest request,
    IInterviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRequisition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCandidate(
        AssociationRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCandidate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCandidate(
    AssociationRequest request,
    IInterviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCandidate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInterviewers(
        MultipleAssociationRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInterviewers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInterviewers(
        MultipleAssociationRequest request,
        IInterviewService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInterviewers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Interview mapRequestToInterview( InterviewRequest request ) {
        var model = new Interview
        {
            Id = request.Id,
            InterviewDate = request.InterviewDate,
            Feedback = request.Feedback,
            Stage = request.Stage,
            Result = request.Result,
        };
        return model;
    }

}
