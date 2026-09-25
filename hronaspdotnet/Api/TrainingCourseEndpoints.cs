
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TrainingCourseEndpoints
{
    public static IEndpointRouteBuilder MapTrainingCourseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trainingCourse").WithTags("TrainingCourses");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToPrerequisites", AddToPrerequisites);
    group.MapPut("/removeFromPrerequisites", RemoveFromPrerequisites);

    group.MapPut("/addToEnrollments", AddToEnrollments);
    group.MapPut("/removeFromEnrollments", RemoveFromEnrollments);

    group.MapPut("/addToJobProfiles", AddToJobProfiles);
    group.MapPut("/removeFromJobProfiles", RemoveFromJobProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        TrainingCourseRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrainingCourse( request );

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
        TrainingCourseRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrainingCourse( request );

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
        ITrainingCourseService service,
        CancellationToken cancellationToken) {

        var trainingCourse = await service.Get(identifier, cancellationToken);
        return trainingCourse is null ? Results.NotFound() : Results.Ok( trainingCourse );
    }


    private static async Task<IResult> GetAll(
        ITrainingCourseService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TrainingCourseResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPrerequisites(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPrerequisites(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPrerequisites(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPrerequisites(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEnrollments(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEnrollments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEnrollments(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEnrollments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToJobProfiles(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToJobProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromJobProfiles(
        MultipleAssociationRequest request,
        ITrainingCourseService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromJobProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TrainingCourse mapRequestToTrainingCourse( TrainingCourseRequest request ) {
        var model = new TrainingCourse
        {
            Id = request.Id,
            Code = request.Code,
            Title = request.Title,
            DurationHours = request.DurationHours,
            DeliveryMethod = request.DeliveryMethod,
        };
        return model;
    }

}
