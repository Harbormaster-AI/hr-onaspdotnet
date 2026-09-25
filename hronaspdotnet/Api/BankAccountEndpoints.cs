
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class BankAccountEndpoints
{
    public static IEndpointRouteBuilder MapBankAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bankAccount").WithTags("BankAccounts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);



        return app;
    }

    private static async Task<IResult> Create(
        BankAccountRequest request,
        IBankAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBankAccount( request );

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
        BankAccountRequest request,
        IBankAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBankAccount( request );

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
        IBankAccountService service,
        CancellationToken cancellationToken) {

        var bankAccount = await service.Get(identifier, cancellationToken);
        return bankAccount is null ? Results.NotFound() : Results.Ok( bankAccount );
    }


    private static async Task<IResult> GetAll(
        IBankAccountService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BankAccountResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBankAccountService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static BankAccount mapRequestToBankAccount( BankAccountRequest request ) {
        var model = new BankAccount
        {
            Id = request.Id,
            AccountHolder = request.AccountHolder,
            BankName = request.BankName,
            Iban = request.Iban,
            Bic = request.Bic,
            AccountNumber = request.AccountNumber,
            RoutingNumber = request.RoutingNumber,
        };
        return model;
    }

}
