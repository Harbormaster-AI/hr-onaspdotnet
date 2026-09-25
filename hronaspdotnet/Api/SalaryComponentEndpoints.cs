
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class SalaryComponentEndpoints
{
    public static IEndpointRouteBuilder MapSalaryComponentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/salaryComponent").WithTags("SalaryComponents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCompensationPackage", AssignCompensationPackage);
        group.MapPut("/unassignCompensationPackage", UnassignCompensationPackage);


        return app;
    }

    private static async Task<IResult> Create(
        SalaryComponentRequest request,
        ISalaryComponentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalaryComponent( request );

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
        SalaryComponentRequest request,
        ISalaryComponentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSalaryComponent( request );

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
        ISalaryComponentService service,
        CancellationToken cancellationToken) {

        var salaryComponent = await service.Get(identifier, cancellationToken);
        return salaryComponent is null ? Results.NotFound() : Results.Ok( salaryComponent );
    }


    private static async Task<IResult> GetAll(
        ISalaryComponentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SalaryComponentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISalaryComponentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCompensationPackage(
        AssociationRequest request,
        ISalaryComponentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCompensationPackage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCompensationPackage(
    AssociationRequest request,
    ISalaryComponentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCompensationPackage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SalaryComponent mapRequestToSalaryComponent( SalaryComponentRequest request ) {
        var model = new SalaryComponent
        {
            Id = request.Id,
            Amount = request.Amount,
            Recurring = request.Recurring,
            ComponentType = request.ComponentType,
        };
        return model;
    }

}
