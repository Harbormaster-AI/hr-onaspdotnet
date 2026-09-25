
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TrainingEnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapTrainingEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trainingEnrollment").WithTags("TrainingEnrollments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCourse", AssignCourse);
        group.MapPut("/unassignCourse", UnassignCourse);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignInstructor", AssignInstructor);
        group.MapPut("/unassignInstructor", UnassignInstructor);


        return app;
    }

    private static async Task<IResult> Create(
        TrainingEnrollmentRequest request,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrainingEnrollment( request );

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
        TrainingEnrollmentRequest request,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrainingEnrollment( request );

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
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {

        var trainingEnrollment = await service.Get(identifier, cancellationToken);
        return trainingEnrollment is null ? Results.NotFound() : Results.Ok( trainingEnrollment );
    }


    private static async Task<IResult> GetAll(
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TrainingEnrollmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCourse(
        AssociationRequest request,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCourse(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCourse(
    AssociationRequest request,
    ITrainingEnrollmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCourse(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ITrainingEnrollmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInstructor(
        AssociationRequest request,
        ITrainingEnrollmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInstructor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInstructor(
    AssociationRequest request,
    ITrainingEnrollmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInstructor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static TrainingEnrollment mapRequestToTrainingEnrollment( TrainingEnrollmentRequest request ) {
        var model = new TrainingEnrollment
        {
            Id = request.Id,
            EnrollmentNumber = request.EnrollmentNumber,
            CompletionDate = request.CompletionDate,
            Score = request.Score,
            Status = request.Status,
        };
        return model;
    }

}
