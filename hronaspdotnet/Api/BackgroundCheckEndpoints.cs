
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class BackgroundCheckEndpoints
{
    public static IEndpointRouteBuilder MapBackgroundCheckEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/backgroundCheck").WithTags("BackgroundChecks");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCandidate", AssignCandidate);
        group.MapPut("/unassignCandidate", UnassignCandidate);
        group.MapPut("/assignRequisition", AssignRequisition);
        group.MapPut("/unassignRequisition", UnassignRequisition);
        group.MapPut("/assignReport", AssignReport);
        group.MapPut("/unassignReport", UnassignReport);


        return app;
    }

    private static async Task<IResult> Create(
        BackgroundCheckRequest request,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBackgroundCheck( request );

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
        BackgroundCheckRequest request,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBackgroundCheck( request );

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
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {

        var backgroundCheck = await service.Get(identifier, cancellationToken);
        return backgroundCheck is null ? Results.NotFound() : Results.Ok( backgroundCheck );
    }


    private static async Task<IResult> GetAll(
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BackgroundCheckResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCandidate(
        AssociationRequest request,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCandidate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCandidate(
    AssociationRequest request,
    IBackgroundCheckService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCandidate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRequisition(
        AssociationRequest request,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRequisition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRequisition(
    AssociationRequest request,
    IBackgroundCheckService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRequisition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignReport(
        AssociationRequest request,
        IBackgroundCheckService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignReport(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignReport(
    AssociationRequest request,
    IBackgroundCheckService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignReport(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static BackgroundCheck mapRequestToBackgroundCheck( BackgroundCheckRequest request ) {
        var model = new BackgroundCheck
        {
            Id = request.Id,
            CheckNumber = request.CheckNumber,
            Provider = request.Provider,
            CompletedDate = request.CompletedDate,
            Status = request.Status,
        };
        return model;
    }

}
