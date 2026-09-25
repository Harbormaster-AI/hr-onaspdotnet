
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CompetencyEndpoints
{
    public static IEndpointRouteBuilder MapCompetencyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/competency").WithTags("Competencys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToJobProfiles", AddToJobProfiles);
    group.MapPut("/removeFromJobProfiles", RemoveFromJobProfiles);

    group.MapPut("/addToCompetencyRatings", AddToCompetencyRatings);
    group.MapPut("/removeFromCompetencyRatings", RemoveFromCompetencyRatings);


        return app;
    }

    private static async Task<IResult> Create(
        CompetencyRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompetency( request );

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
        CompetencyRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompetency( request );

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
        ICompetencyService service,
        CancellationToken cancellationToken) {

        var competency = await service.Get(identifier, cancellationToken);
        return competency is null ? Results.NotFound() : Results.Ok( competency );
    }


    private static async Task<IResult> GetAll(
        ICompetencyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CompetencyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICompetencyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToJobProfiles(
        MultipleAssociationRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToJobProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromJobProfiles(
        MultipleAssociationRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromJobProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCompetencyRatings(
        MultipleAssociationRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompetencyRatings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompetencyRatings(
        MultipleAssociationRequest request,
        ICompetencyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompetencyRatings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Competency mapRequestToCompetency( CompetencyRequest request ) {
        var model = new Competency
        {
            Id = request.Id,
            Name = request.Name,
            Category = request.Category,
        };
        return model;
    }

}
