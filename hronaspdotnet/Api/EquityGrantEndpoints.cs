
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class EquityGrantEndpoints
{
    public static IEndpointRouteBuilder MapEquityGrantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/equityGrant").WithTags("EquityGrants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCompensationPackage", AssignCompensationPackage);
        group.MapPut("/unassignCompensationPackage", UnassignCompensationPackage);


        return app;
    }

    private static async Task<IResult> Create(
        EquityGrantRequest request,
        IEquityGrantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEquityGrant( request );

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
        EquityGrantRequest request,
        IEquityGrantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEquityGrant( request );

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
        IEquityGrantService service,
        CancellationToken cancellationToken) {

        var equityGrant = await service.Get(identifier, cancellationToken);
        return equityGrant is null ? Results.NotFound() : Results.Ok( equityGrant );
    }


    private static async Task<IResult> GetAll(
        IEquityGrantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EquityGrantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEquityGrantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCompensationPackage(
        AssociationRequest request,
        IEquityGrantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCompensationPackage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCompensationPackage(
    AssociationRequest request,
    IEquityGrantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCompensationPackage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static EquityGrant mapRequestToEquityGrant( EquityGrantRequest request ) {
        var model = new EquityGrant
        {
            Id = request.Id,
            GrantId = request.GrantId,
            GrantedUnits = request.GrantedUnits,
            VestingStart = request.VestingStart,
            GrantType = request.GrantType,
        };
        return model;
    }

}
