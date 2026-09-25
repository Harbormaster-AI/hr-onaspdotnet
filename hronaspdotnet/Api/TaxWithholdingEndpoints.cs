
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TaxWithholdingEndpoints
{
    public static IEndpointRouteBuilder MapTaxWithholdingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/taxWithholding").WithTags("TaxWithholdings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        TaxWithholdingRequest request,
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTaxWithholding( request );

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
        TaxWithholdingRequest request,
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTaxWithholding( request );

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
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {

        var taxWithholding = await service.Get(identifier, cancellationToken);
        return taxWithholding is null ? Results.NotFound() : Results.Ok( taxWithholding );
    }


    private static async Task<IResult> GetAll(
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TaxWithholdingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ITaxWithholdingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ITaxWithholdingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static TaxWithholding mapRequestToTaxWithholding( TaxWithholdingRequest request ) {
        var model = new TaxWithholding
        {
            Id = request.Id,
            TaxId = request.TaxId,
            Allowances = request.Allowances,
            AdditionalAmount = request.AdditionalAmount,
            FilingStatus = request.FilingStatus,
        };
        return model;
    }

}
