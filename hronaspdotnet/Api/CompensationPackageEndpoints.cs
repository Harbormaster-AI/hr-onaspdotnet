
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CompensationPackageEndpoints
{
    public static IEndpointRouteBuilder MapCompensationPackageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/compensationPackage").WithTags("CompensationPackages");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignContract", AssignContract);
        group.MapPut("/unassignContract", UnassignContract);

    group.MapPut("/addToSalaryComponents", AddToSalaryComponents);
    group.MapPut("/removeFromSalaryComponents", RemoveFromSalaryComponents);

    group.MapPut("/addToBonusPlans", AddToBonusPlans);
    group.MapPut("/removeFromBonusPlans", RemoveFromBonusPlans);

    group.MapPut("/addToEquityGrants", AddToEquityGrants);
    group.MapPut("/removeFromEquityGrants", RemoveFromEquityGrants);


        return app;
    }

    private static async Task<IResult> Create(
        CompensationPackageRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompensationPackage( request );

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
        CompensationPackageRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompensationPackage( request );

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
        ICompensationPackageService service,
        CancellationToken cancellationToken) {

        var compensationPackage = await service.Get(identifier, cancellationToken);
        return compensationPackage is null ? Results.NotFound() : Results.Ok( compensationPackage );
    }


    private static async Task<IResult> GetAll(
        ICompensationPackageService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CompensationPackageResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignContract(
        AssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignContract(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignContract(
    AssociationRequest request,
    ICompensationPackageService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignContract(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSalaryComponents(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSalaryComponents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSalaryComponents(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSalaryComponents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBonusPlans(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBonusPlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBonusPlans(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBonusPlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEquityGrants(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEquityGrants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEquityGrants(
        MultipleAssociationRequest request,
        ICompensationPackageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEquityGrants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CompensationPackage mapRequestToCompensationPackage( CompensationPackageRequest request ) {
        var model = new CompensationPackage
        {
            Id = request.Id,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
            Currency = request.Currency,
        };
        return model;
    }

}
