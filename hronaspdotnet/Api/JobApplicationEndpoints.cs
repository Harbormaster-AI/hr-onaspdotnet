
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class JobApplicationEndpoints
{
    public static IEndpointRouteBuilder MapJobApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/jobApplication").WithTags("JobApplications");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCandidate", AssignCandidate);
        group.MapPut("/unassignCandidate", UnassignCandidate);
        group.MapPut("/assignRequisition", AssignRequisition);
        group.MapPut("/unassignRequisition", UnassignRequisition);

    group.MapPut("/addToScreenings", AddToScreenings);
    group.MapPut("/removeFromScreenings", RemoveFromScreenings);


        return app;
    }

    private static async Task<IResult> Create(
        JobApplicationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobApplication( request );

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
        JobApplicationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobApplication( request );

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
        IJobApplicationService service,
        CancellationToken cancellationToken) {

        var jobApplication = await service.Get(identifier, cancellationToken);
        return jobApplication is null ? Results.NotFound() : Results.Ok( jobApplication );
    }


    private static async Task<IResult> GetAll(
        IJobApplicationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( JobApplicationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IJobApplicationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCandidate(
        AssociationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCandidate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCandidate(
    AssociationRequest request,
    IJobApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCandidate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRequisition(
        AssociationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRequisition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRequisition(
    AssociationRequest request,
    IJobApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRequisition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToScreenings(
        MultipleAssociationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToScreenings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromScreenings(
        MultipleAssociationRequest request,
        IJobApplicationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromScreenings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static JobApplication mapRequestToJobApplication( JobApplicationRequest request ) {
        var model = new JobApplication
        {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            AppliedDate = request.AppliedDate,
            ResumeUrl = request.ResumeUrl,
            Status = request.Status,
        };
        return model;
    }

}
