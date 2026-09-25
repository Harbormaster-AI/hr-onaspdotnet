
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PayrollRunEndpoints
{
    public static IEndpointRouteBuilder MapPayrollRunEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payrollRun").WithTags("PayrollRuns");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPayrollCalendar", AssignPayrollCalendar);
        group.MapPut("/unassignPayrollCalendar", UnassignPayrollCalendar);

    group.MapPut("/addToPayrollItems", AddToPayrollItems);
    group.MapPut("/removeFromPayrollItems", RemoveFromPayrollItems);


        return app;
    }

    private static async Task<IResult> Create(
        PayrollRunRequest request,
        IPayrollRunService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollRun( request );

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
        PayrollRunRequest request,
        IPayrollRunService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollRun( request );

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
        IPayrollRunService service,
        CancellationToken cancellationToken) {

        var payrollRun = await service.Get(identifier, cancellationToken);
        return payrollRun is null ? Results.NotFound() : Results.Ok( payrollRun );
    }


    private static async Task<IResult> GetAll(
        IPayrollRunService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PayrollRunResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPayrollRunService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayrollCalendar(
        AssociationRequest request,
        IPayrollRunService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayrollCalendar(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayrollCalendar(
    AssociationRequest request,
    IPayrollRunService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayrollCalendar(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPayrollItems(
        MultipleAssociationRequest request,
        IPayrollRunService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayrollItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayrollItems(
        MultipleAssociationRequest request,
        IPayrollRunService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayrollItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PayrollRun mapRequestToPayrollRun( PayrollRunRequest request ) {
        var model = new PayrollRun
        {
            Id = request.Id,
            RunNumber = request.RunNumber,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            PaymentDate = request.PaymentDate,
            Status = request.Status,
        };
        return model;
    }

}
