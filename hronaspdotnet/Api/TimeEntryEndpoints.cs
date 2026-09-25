
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TimeEntryEndpoints
{
    public static IEndpointRouteBuilder MapTimeEntryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timeEntry").WithTags("TimeEntrys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignTimesheet", AssignTimesheet);
        group.MapPut("/unassignTimesheet", UnassignTimesheet);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignCostCenter", AssignCostCenter);
        group.MapPut("/unassignCostCenter", UnassignCostCenter);


        return app;
    }

    private static async Task<IResult> Create(
        TimeEntryRequest request,
        ITimeEntryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTimeEntry( request );

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
        TimeEntryRequest request,
        ITimeEntryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTimeEntry( request );

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
        ITimeEntryService service,
        CancellationToken cancellationToken) {

        var timeEntry = await service.Get(identifier, cancellationToken);
        return timeEntry is null ? Results.NotFound() : Results.Ok( timeEntry );
    }


    private static async Task<IResult> GetAll(
        ITimeEntryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TimeEntryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITimeEntryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTimesheet(
        AssociationRequest request,
        ITimeEntryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTimesheet(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTimesheet(
    AssociationRequest request,
    ITimeEntryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTimesheet(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ITimeEntryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ITimeEntryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCostCenter(
        AssociationRequest request,
        ITimeEntryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCostCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCostCenter(
    AssociationRequest request,
    ITimeEntryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCostCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static TimeEntry mapRequestToTimeEntry( TimeEntryRequest request ) {
        var model = new TimeEntry
        {
            Id = request.Id,
            EntryDate = request.EntryDate,
            HoursWorked = request.HoursWorked,
            EntryType = request.EntryType,
        };
        return model;
    }

}
