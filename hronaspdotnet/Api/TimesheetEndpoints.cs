
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TimesheetEndpoints
{
    public static IEndpointRouteBuilder MapTimesheetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timesheet").WithTags("Timesheets");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);

    group.MapPut("/addToTimeEntries", AddToTimeEntries);
    group.MapPut("/removeFromTimeEntries", RemoveFromTimeEntries);

    group.MapPut("/addToApprovals", AddToApprovals);
    group.MapPut("/removeFromApprovals", RemoveFromApprovals);


        return app;
    }

    private static async Task<IResult> Create(
        TimesheetRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTimesheet( request );

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
        TimesheetRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTimesheet( request );

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
        ITimesheetService service,
        CancellationToken cancellationToken) {

        var timesheet = await service.Get(identifier, cancellationToken);
        return timesheet is null ? Results.NotFound() : Results.Ok( timesheet );
    }


    private static async Task<IResult> GetAll(
        ITimesheetService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TimesheetResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ITimesheetService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTimeEntries(
        MultipleAssociationRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTimeEntries(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTimeEntries(
        MultipleAssociationRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTimeEntries(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToApprovals(
        MultipleAssociationRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApprovals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApprovals(
        MultipleAssociationRequest request,
        ITimesheetService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApprovals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Timesheet mapRequestToTimesheet( TimesheetRequest request ) {
        var model = new Timesheet
        {
            Id = request.Id,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            SubmissionDate = request.SubmissionDate,
            Status = request.Status,
        };
        return model;
    }

}
