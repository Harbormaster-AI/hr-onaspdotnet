
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class JobProfileEndpoints
{
    public static IEndpointRouteBuilder MapJobProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/jobProfile").WithTags("JobProfiles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignJobFamily", AssignJobFamily);
        group.MapPut("/unassignJobFamily", UnassignJobFamily);

    group.MapPut("/addToCompetencies", AddToCompetencies);
    group.MapPut("/removeFromCompetencies", RemoveFromCompetencies);

    group.MapPut("/addToTrainingRecommendations", AddToTrainingRecommendations);
    group.MapPut("/removeFromTrainingRecommendations", RemoveFromTrainingRecommendations);

    group.MapPut("/addToPositions", AddToPositions);
    group.MapPut("/removeFromPositions", RemoveFromPositions);


        return app;
    }

    private static async Task<IResult> Create(
        JobProfileRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobProfile( request );

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
        JobProfileRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobProfile( request );

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
        IJobProfileService service,
        CancellationToken cancellationToken) {

        var jobProfile = await service.Get(identifier, cancellationToken);
        return jobProfile is null ? Results.NotFound() : Results.Ok( jobProfile );
    }


    private static async Task<IResult> GetAll(
        IJobProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( JobProfileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignJobFamily(
        AssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignJobFamily(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignJobFamily(
    AssociationRequest request,
    IJobProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignJobFamily(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCompetencies(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompetencies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompetencies(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompetencies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTrainingRecommendations(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrainingRecommendations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrainingRecommendations(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrainingRecommendations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPositions(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPositions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPositions(
        MultipleAssociationRequest request,
        IJobProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPositions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static JobProfile mapRequestToJobProfile( JobProfileRequest request ) {
        var model = new JobProfile
        {
            Id = request.Id,
            Title = request.Title,
            JobCode = request.JobCode,
            JobLevel = request.JobLevel,
            ExemptStatus = request.ExemptStatus,
        };
        return model;
    }

}
