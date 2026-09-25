
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class WorkScheduleEndpoints
{
    public static IEndpointRouteBuilder MapWorkScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workSchedule").WithTags("WorkSchedules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);

    group.MapPut("/addToShifts", AddToShifts);
    group.MapPut("/removeFromShifts", RemoveFromShifts);

    group.MapPut("/addToExceptions", AddToExceptions);
    group.MapPut("/removeFromExceptions", RemoveFromExceptions);


        return app;
    }

    private static async Task<IResult> Create(
        WorkScheduleRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkSchedule( request );

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
        WorkScheduleRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkSchedule( request );

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
        IWorkScheduleService service,
        CancellationToken cancellationToken) {

        var workSchedule = await service.Get(identifier, cancellationToken);
        return workSchedule is null ? Results.NotFound() : Results.Ok( workSchedule );
    }


    private static async Task<IResult> GetAll(
        IWorkScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WorkScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToShifts(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShifts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShifts(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShifts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToExceptions(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToExceptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromExceptions(
        MultipleAssociationRequest request,
        IWorkScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromExceptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static WorkSchedule mapRequestToWorkSchedule( WorkScheduleRequest request ) {
        var model = new WorkSchedule
        {
            Id = request.Id,
            Name = request.Name,
            StandardHoursPerWeek = request.StandardHoursPerWeek,
            ScheduleType = request.ScheduleType,
        };
        return model;
    }

}
