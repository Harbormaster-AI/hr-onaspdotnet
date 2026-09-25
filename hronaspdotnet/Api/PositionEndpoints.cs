
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PositionEndpoints
{
    public static IEndpointRouteBuilder MapPositionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/position").WithTags("Positions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDepartment", AssignDepartment);
        group.MapPut("/unassignDepartment", UnassignDepartment);
        group.MapPut("/assignJobProfile", AssignJobProfile);
        group.MapPut("/unassignJobProfile", UnassignJobProfile);
        group.MapPut("/assignCostCenter", AssignCostCenter);
        group.MapPut("/unassignCostCenter", UnassignCostCenter);
        group.MapPut("/assignLocation", AssignLocation);
        group.MapPut("/unassignLocation", UnassignLocation);
        group.MapPut("/assignManagerPosition", AssignManagerPosition);
        group.MapPut("/unassignManagerPosition", UnassignManagerPosition);

    group.MapPut("/addToDirectReports", AddToDirectReports);
    group.MapPut("/removeFromDirectReports", RemoveFromDirectReports);

    group.MapPut("/addToAssignments", AddToAssignments);
    group.MapPut("/removeFromAssignments", RemoveFromAssignments);


        return app;
    }

    private static async Task<IResult> Create(
        PositionRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPosition( request );

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
        PositionRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPosition( request );

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
        IPositionService service,
        CancellationToken cancellationToken) {

        var position = await service.Get(identifier, cancellationToken);
        return position is null ? Results.NotFound() : Results.Ok( position );
    }


    private static async Task<IResult> GetAll(
        IPositionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PositionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPositionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDepartment(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDepartment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDepartment(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDepartment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignJobProfile(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignJobProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignJobProfile(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignJobProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCostCenter(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCostCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCostCenter(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCostCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLocation(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLocation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLocation(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLocation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignManagerPosition(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignManagerPosition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignManagerPosition(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignManagerPosition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDirectReports(
        MultipleAssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDirectReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDirectReports(
        MultipleAssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDirectReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAssignments(
        MultipleAssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAssignments(
        MultipleAssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Position mapRequestToPosition( PositionRequest request ) {
        var model = new Position
        {
            Id = request.Id,
            PositionCode = request.PositionCode,
            Fte = request.Fte,
            Status = request.Status,
            WorkLocationType = request.WorkLocationType,
        };
        return model;
    }

}
