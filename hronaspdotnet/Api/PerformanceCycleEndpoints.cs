
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PerformanceCycleEndpoints
{
    public static IEndpointRouteBuilder MapPerformanceCycleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/performanceCycle").WithTags("PerformanceCycles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToReviews", AddToReviews);
    group.MapPut("/removeFromReviews", RemoveFromReviews);

    group.MapPut("/addToGoals", AddToGoals);
    group.MapPut("/removeFromGoals", RemoveFromGoals);


        return app;
    }

    private static async Task<IResult> Create(
        PerformanceCycleRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceCycle( request );

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
        PerformanceCycleRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceCycle( request );

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
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {

        var performanceCycle = await service.Get(identifier, cancellationToken);
        return performanceCycle is null ? Results.NotFound() : Results.Ok( performanceCycle );
    }


    private static async Task<IResult> GetAll(
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PerformanceCycleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IPerformanceCycleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToReviews(
        MultipleAssociationRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReviews(
        MultipleAssociationRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGoals(
        MultipleAssociationRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGoals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGoals(
        MultipleAssociationRequest request,
        IPerformanceCycleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGoals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PerformanceCycle mapRequestToPerformanceCycle( PerformanceCycleRequest request ) {
        var model = new PerformanceCycle
        {
            Id = request.Id,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
        return model;
    }

}
