
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class LocationEndpoints
{
    public static IEndpointRouteBuilder MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/location").WithTags("Locations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToDepartments", AddToDepartments);
    group.MapPut("/removeFromDepartments", RemoveFromDepartments);

    group.MapPut("/addToPositions", AddToPositions);
    group.MapPut("/removeFromPositions", RemoveFromPositions);

    group.MapPut("/addToEmployees", AddToEmployees);
    group.MapPut("/removeFromEmployees", RemoveFromEmployees);


        return app;
    }

    private static async Task<IResult> Create(
        LocationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLocation( request );

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
        LocationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLocation( request );

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
        ILocationService service,
        CancellationToken cancellationToken) {

        var location = await service.Get(identifier, cancellationToken);
        return location is null ? Results.NotFound() : Results.Ok( location );
    }


    private static async Task<IResult> GetAll(
        ILocationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LocationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILocationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    ILocationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDepartments(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDepartments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDepartments(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDepartments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPositions(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPositions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPositions(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPositions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEmployees(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEmployees(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEmployees(
        MultipleAssociationRequest request,
        ILocationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEmployees(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Location mapRequestToLocation( LocationRequest request ) {
        var model = new Location
        {
            Id = request.Id,
            Name = request.Name,
            Address = request.Address,
            Timezone = request.Timezone,
        };
        return model;
    }

}
