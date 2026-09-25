
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PaymentMethodEndpoints
{
    public static IEndpointRouteBuilder MapPaymentMethodEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentMethod").WithTags("PaymentMethods");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignBankAccount", AssignBankAccount);
        group.MapPut("/unassignBankAccount", UnassignBankAccount);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentMethodRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentMethod( request );

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
        PaymentMethodRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentMethod( request );

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
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var paymentMethod = await service.Get(identifier, cancellationToken);
        return paymentMethod is null ? Results.NotFound() : Results.Ok( paymentMethod );
    }


    private static async Task<IResult> GetAll(
        IPaymentMethodService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentMethodResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IPaymentMethodService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBankAccount(
        AssociationRequest request,
        IPaymentMethodService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBankAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBankAccount(
    AssociationRequest request,
    IPaymentMethodService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBankAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PaymentMethod mapRequestToPaymentMethod( PaymentMethodRequest request ) {
        var model = new PaymentMethod
        {
            Id = request.Id,
            Preferred = request.Preferred,
            MethodType = request.MethodType,
        };
        return model;
    }

}
