
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CompetencyRatingEndpoints
{
    public static IEndpointRouteBuilder MapCompetencyRatingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/competencyRating").WithTags("CompetencyRatings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignReview", AssignReview);
        group.MapPut("/unassignReview", UnassignReview);
        group.MapPut("/assignCompetency", AssignCompetency);
        group.MapPut("/unassignCompetency", UnassignCompetency);


        return app;
    }

    private static async Task<IResult> Create(
        CompetencyRatingRequest request,
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompetencyRating( request );

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
        CompetencyRatingRequest request,
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompetencyRating( request );

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
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {

        var competencyRating = await service.Get(identifier, cancellationToken);
        return competencyRating is null ? Results.NotFound() : Results.Ok( competencyRating );
    }


    private static async Task<IResult> GetAll(
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CompetencyRatingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignReview(
        AssociationRequest request,
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignReview(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignReview(
    AssociationRequest request,
    ICompetencyRatingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignReview(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCompetency(
        AssociationRequest request,
        ICompetencyRatingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCompetency(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCompetency(
    AssociationRequest request,
    ICompetencyRatingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCompetency(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CompetencyRating mapRequestToCompetencyRating( CompetencyRatingRequest request ) {
        var model = new CompetencyRating
        {
            Id = request.Id,
            Comment = request.Comment,
            Rating = request.Rating,
        };
        return model;
    }

}
