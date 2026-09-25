
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class EmploymentAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapEmploymentAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employmentAssignment").WithTags("EmploymentAssignments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignPosition", AssignPosition);
        group.MapPut("/unassignPosition", UnassignPosition);
        group.MapPut("/assignSupervisor", AssignSupervisor);
        group.MapPut("/unassignSupervisor", UnassignSupervisor);


        return app;
    }

    private static async Task<IResult> Create(
        EmploymentAssignmentRequest request,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmploymentAssignment( request );

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
        EmploymentAssignmentRequest request,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmploymentAssignment( request );

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
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {

        var employmentAssignment = await service.Get(identifier, cancellationToken);
        return employmentAssignment is null ? Results.NotFound() : Results.Ok( employmentAssignment );
    }


    private static async Task<IResult> GetAll(
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EmploymentAssignmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IEmploymentAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPosition(
        AssociationRequest request,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPosition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPosition(
    AssociationRequest request,
    IEmploymentAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPosition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSupervisor(
        AssociationRequest request,
        IEmploymentAssignmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSupervisor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSupervisor(
    AssociationRequest request,
    IEmploymentAssignmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSupervisor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static EmploymentAssignment mapRequestToEmploymentAssignment( EmploymentAssignmentRequest request ) {
        var model = new EmploymentAssignment
        {
            Id = request.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Primary = request.Primary,
            AssignmentType = request.AssignmentType,
            Status = request.Status,
        };
        return model;
    }

}
