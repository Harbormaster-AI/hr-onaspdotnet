
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class DepartmentEndpoints
{
    public static IEndpointRouteBuilder MapDepartmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/department").WithTags("Departments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);
        group.MapPut("/assignManager", AssignManager);
        group.MapPut("/unassignManager", UnassignManager);
        group.MapPut("/assignCostCenter", AssignCostCenter);
        group.MapPut("/unassignCostCenter", UnassignCostCenter);

    group.MapPut("/addToPositions", AddToPositions);
    group.MapPut("/removeFromPositions", RemoveFromPositions);

    group.MapPut("/addToEmployees", AddToEmployees);
    group.MapPut("/removeFromEmployees", RemoveFromEmployees);


        return app;
    }

    private static async Task<IResult> Create(
        DepartmentRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDepartment( request );

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
        DepartmentRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDepartment( request );

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
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var department = await service.Get(identifier, cancellationToken);
        return department is null ? Results.NotFound() : Results.Ok( department );
    }


    private static async Task<IResult> GetAll(
        IDepartmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DepartmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IDepartmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignManager(
        AssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignManager(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignManager(
    AssociationRequest request,
    IDepartmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignManager(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCostCenter(
        AssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCostCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCostCenter(
    AssociationRequest request,
    IDepartmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCostCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPositions(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPositions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPositions(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPositions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEmployees(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEmployees(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEmployees(
        MultipleAssociationRequest request,
        IDepartmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEmployees(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Department mapRequestToDepartment( DepartmentRequest request ) {
        var model = new Department
        {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
        };
        return model;
    }

}
