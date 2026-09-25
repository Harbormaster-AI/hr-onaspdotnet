
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PerformanceReviewEndpoints
{
    public static IEndpointRouteBuilder MapPerformanceReviewEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/performanceReview").WithTags("PerformanceReviews");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignReviewer", AssignReviewer);
        group.MapPut("/unassignReviewer", UnassignReviewer);
        group.MapPut("/assignCycle", AssignCycle);
        group.MapPut("/unassignCycle", UnassignCycle);

    group.MapPut("/addToCompetencyRatings", AddToCompetencyRatings);
    group.MapPut("/removeFromCompetencyRatings", RemoveFromCompetencyRatings);

    group.MapPut("/addToGoals", AddToGoals);
    group.MapPut("/removeFromGoals", RemoveFromGoals);


        return app;
    }

    private static async Task<IResult> Create(
        PerformanceReviewRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceReview( request );

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
        PerformanceReviewRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPerformanceReview( request );

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
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {

        var performanceReview = await service.Get(identifier, cancellationToken);
        return performanceReview is null ? Results.NotFound() : Results.Ok( performanceReview );
    }


    private static async Task<IResult> GetAll(
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PerformanceReviewResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IPerformanceReviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignReviewer(
        AssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignReviewer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignReviewer(
    AssociationRequest request,
    IPerformanceReviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignReviewer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCycle(
        AssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCycle(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCycle(
    AssociationRequest request,
    IPerformanceReviewService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCycle(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCompetencyRatings(
        MultipleAssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompetencyRatings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompetencyRatings(
        MultipleAssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompetencyRatings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGoals(
        MultipleAssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGoals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGoals(
        MultipleAssociationRequest request,
        IPerformanceReviewService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGoals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PerformanceReview mapRequestToPerformanceReview( PerformanceReviewRequest request ) {
        var model = new PerformanceReview
        {
            Id = request.Id,
            ReviewNumber = request.ReviewNumber,
            ReviewDate = request.ReviewDate,
            ReviewerComments = request.ReviewerComments,
            Rating = request.Rating,
            Status = request.Status,
        };
        return model;
    }

}
