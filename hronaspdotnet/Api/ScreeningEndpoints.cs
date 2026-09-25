
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class ScreeningEndpoints
{
    public static IEndpointRouteBuilder MapScreeningEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/screening").WithTags("Screenings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignApplication", AssignApplication);
        group.MapPut("/unassignApplication", UnassignApplication);


        return app;
    }

    private static async Task<IResult> Create(
        ScreeningRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScreening( request );

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
        ScreeningRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScreening( request );

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
        IScreeningService service,
        CancellationToken cancellationToken) {

        var screening = await service.Get(identifier, cancellationToken);
        return screening is null ? Results.NotFound() : Results.Ok( screening );
    }


    private static async Task<IResult> GetAll(
        IScreeningService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ScreeningResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApplication(
        AssociationRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApplication(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApplication(
    AssociationRequest request,
    IScreeningService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApplication(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Screening mapRequestToScreening( ScreeningRequest request ) {
        var model = new Screening
        {
            Id = request.Id,
            Name = request.Name,
            CompletedDate = request.CompletedDate,
            Status = request.Status,
        };
        return model;
    }

}
