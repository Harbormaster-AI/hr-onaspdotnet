
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class WorkShiftEndpoints
{
    public static IEndpointRouteBuilder MapWorkShiftEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workShift").WithTags("WorkShifts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignWorkSchedule", AssignWorkSchedule);
        group.MapPut("/unassignWorkSchedule", UnassignWorkSchedule);


        return app;
    }

    private static async Task<IResult> Create(
        WorkShiftRequest request,
        IWorkShiftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkShift( request );

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
        WorkShiftRequest request,
        IWorkShiftService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkShift( request );

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
        IWorkShiftService service,
        CancellationToken cancellationToken) {

        var workShift = await service.Get(identifier, cancellationToken);
        return workShift is null ? Results.NotFound() : Results.Ok( workShift );
    }


    private static async Task<IResult> GetAll(
        IWorkShiftService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WorkShiftResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWorkShiftService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkSchedule(
        AssociationRequest request,
        IWorkShiftService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkSchedule(
    AssociationRequest request,
    IWorkShiftService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static WorkShift mapRequestToWorkShift( WorkShiftRequest request ) {
        var model = new WorkShift
        {
            Id = request.Id,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            BreakMinutes = request.BreakMinutes,
            DayOfWeek_ = request.DayOfWeek_,
        };
        return model;
    }

}
