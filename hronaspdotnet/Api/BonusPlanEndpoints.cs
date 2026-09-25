
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class BonusPlanEndpoints
{
    public static IEndpointRouteBuilder MapBonusPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bonusPlan").WithTags("BonusPlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToCompensationPackages", AddToCompensationPackages);
    group.MapPut("/removeFromCompensationPackages", RemoveFromCompensationPackages);


        return app;
    }

    private static async Task<IResult> Create(
        BonusPlanRequest request,
        IBonusPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBonusPlan( request );

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
        BonusPlanRequest request,
        IBonusPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBonusPlan( request );

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
        IBonusPlanService service,
        CancellationToken cancellationToken) {

        var bonusPlan = await service.Get(identifier, cancellationToken);
        return bonusPlan is null ? Results.NotFound() : Results.Ok( bonusPlan );
    }


    private static async Task<IResult> GetAll(
        IBonusPlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BonusPlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBonusPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCompensationPackages(
        MultipleAssociationRequest request,
        IBonusPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompensationPackages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompensationPackages(
        MultipleAssociationRequest request,
        IBonusPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompensationPackages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BonusPlan mapRequestToBonusPlan( BonusPlanRequest request ) {
        var model = new BonusPlan
        {
            Id = request.Id,
            Name = request.Name,
            TargetPercentage = request.TargetPercentage,
        };
        return model;
    }

}
