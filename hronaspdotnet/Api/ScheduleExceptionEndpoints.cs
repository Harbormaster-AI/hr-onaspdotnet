
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class ScheduleExceptionEndpoints
{
    public static IEndpointRouteBuilder MapScheduleExceptionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/scheduleException").WithTags("ScheduleExceptions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignWorkSchedule", AssignWorkSchedule);
        group.MapPut("/unassignWorkSchedule", UnassignWorkSchedule);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        ScheduleExceptionRequest request,
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScheduleException( request );

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
        ScheduleExceptionRequest request,
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScheduleException( request );

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
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {

        var scheduleException = await service.Get(identifier, cancellationToken);
        return scheduleException is null ? Results.NotFound() : Results.Ok( scheduleException );
    }


    private static async Task<IResult> GetAll(
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ScheduleExceptionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkSchedule(
        AssociationRequest request,
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkSchedule(
    AssociationRequest request,
    IScheduleExceptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IScheduleExceptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IScheduleExceptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ScheduleException mapRequestToScheduleException( ScheduleExceptionRequest request ) {
        var model = new ScheduleException
        {
            Id = request.Id,
            Date = request.Date,
            Reason = request.Reason,
            Hours = request.Hours,
        };
        return model;
    }

}
