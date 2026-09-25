
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PayrollItemEndpoints
{
    public static IEndpointRouteBuilder MapPayrollItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payrollItem").WithTags("PayrollItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPayrollRun", AssignPayrollRun);
        group.MapPut("/unassignPayrollRun", UnassignPayrollRun);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        PayrollItemRequest request,
        IPayrollItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollItem( request );

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
        PayrollItemRequest request,
        IPayrollItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollItem( request );

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
        IPayrollItemService service,
        CancellationToken cancellationToken) {

        var payrollItem = await service.Get(identifier, cancellationToken);
        return payrollItem is null ? Results.NotFound() : Results.Ok( payrollItem );
    }


    private static async Task<IResult> GetAll(
        IPayrollItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PayrollItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPayrollItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayrollRun(
        AssociationRequest request,
        IPayrollItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayrollRun(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayrollRun(
    AssociationRequest request,
    IPayrollItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayrollRun(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IPayrollItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IPayrollItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PayrollItem mapRequestToPayrollItem( PayrollItemRequest request ) {
        var model = new PayrollItem
        {
            Id = request.Id,
            Amount = request.Amount,
            Taxable = request.Taxable,
            ItemType = request.ItemType,
        };
        return model;
    }

}
