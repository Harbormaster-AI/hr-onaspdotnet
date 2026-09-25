
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CostCenterEndpoints
{
    public static IEndpointRouteBuilder MapCostCenterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/costCenter").WithTags("CostCenters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToDepartments", AddToDepartments);
    group.MapPut("/removeFromDepartments", RemoveFromDepartments);

    group.MapPut("/addToPositions", AddToPositions);
    group.MapPut("/removeFromPositions", RemoveFromPositions);

    group.MapPut("/addToEmployees", AddToEmployees);
    group.MapPut("/removeFromEmployees", RemoveFromEmployees);


        return app;
    }

    private static async Task<IResult> Create(
        CostCenterRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCostCenter( request );

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
        CostCenterRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCostCenter( request );

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
        ICostCenterService service,
        CancellationToken cancellationToken) {

        var costCenter = await service.Get(identifier, cancellationToken);
        return costCenter is null ? Results.NotFound() : Results.Ok( costCenter );
    }


    private static async Task<IResult> GetAll(
        ICostCenterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CostCenterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    ICostCenterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDepartments(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDepartments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDepartments(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDepartments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPositions(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPositions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPositions(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPositions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEmployees(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEmployees(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEmployees(
        MultipleAssociationRequest request,
        ICostCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEmployees(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CostCenter mapRequestToCostCenter( CostCenterRequest request ) {
        var model = new CostCenter
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
        };
        return model;
    }

}
