
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class EmployeeEndpoints
{
    public static IEndpointRouteBuilder MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employee").WithTags("Employees");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignManager", AssignManager);
        group.MapPut("/unassignManager", UnassignManager);
        group.MapPut("/assignDepartment", AssignDepartment);
        group.MapPut("/unassignDepartment", UnassignDepartment);
        group.MapPut("/assignPrimaryLocation", AssignPrimaryLocation);
        group.MapPut("/unassignPrimaryLocation", UnassignPrimaryLocation);
        group.MapPut("/assignCostCenter", AssignCostCenter);
        group.MapPut("/unassignCostCenter", UnassignCostCenter);

    group.MapPut("/addToDirectReports", AddToDirectReports);
    group.MapPut("/removeFromDirectReports", RemoveFromDirectReports);

    group.MapPut("/addToEmploymentAssignments", AddToEmploymentAssignments);
    group.MapPut("/removeFromEmploymentAssignments", RemoveFromEmploymentAssignments);

    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);

    group.MapPut("/addToBenefitEnrollments", AddToBenefitEnrollments);
    group.MapPut("/removeFromBenefitEnrollments", RemoveFromBenefitEnrollments);

    group.MapPut("/addToTimesheets", AddToTimesheets);
    group.MapPut("/removeFromTimesheets", RemoveFromTimesheets);

    group.MapPut("/addToLeaveRequests", AddToLeaveRequests);
    group.MapPut("/removeFromLeaveRequests", RemoveFromLeaveRequests);

    group.MapPut("/addToPerformanceReviews", AddToPerformanceReviews);
    group.MapPut("/removeFromPerformanceReviews", RemoveFromPerformanceReviews);

    group.MapPut("/addToTrainingEnrollments", AddToTrainingEnrollments);
    group.MapPut("/removeFromTrainingEnrollments", RemoveFromTrainingEnrollments);

    group.MapPut("/addToWorkAuthorizations", AddToWorkAuthorizations);
    group.MapPut("/removeFromWorkAuthorizations", RemoveFromWorkAuthorizations);


        return app;
    }

    private static async Task<IResult> Create(
        EmployeeRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmployee( request );

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
        EmployeeRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmployee( request );

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
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var employee = await service.Get(identifier, cancellationToken);
        return employee is null ? Results.NotFound() : Results.Ok( employee );
    }


    private static async Task<IResult> GetAll(
        IEmployeeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EmployeeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignManager(
        AssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignManager(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignManager(
    AssociationRequest request,
    IEmployeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignManager(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDepartment(
        AssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDepartment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDepartment(
    AssociationRequest request,
    IEmployeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDepartment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPrimaryLocation(
        AssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPrimaryLocation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPrimaryLocation(
    AssociationRequest request,
    IEmployeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPrimaryLocation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCostCenter(
        AssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCostCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCostCenter(
    AssociationRequest request,
    IEmployeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCostCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDirectReports(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDirectReports(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDirectReports(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDirectReports(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEmploymentAssignments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEmploymentAssignments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEmploymentAssignments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEmploymentAssignments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBenefitEnrollments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBenefitEnrollments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBenefitEnrollments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBenefitEnrollments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTimesheets(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTimesheets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTimesheets(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTimesheets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLeaveRequests(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLeaveRequests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLeaveRequests(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLeaveRequests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPerformanceReviews(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPerformanceReviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPerformanceReviews(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPerformanceReviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTrainingEnrollments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrainingEnrollments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrainingEnrollments(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrainingEnrollments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWorkAuthorizations(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWorkAuthorizations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWorkAuthorizations(
        MultipleAssociationRequest request,
        IEmployeeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWorkAuthorizations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Employee mapRequestToEmployee( EmployeeRequest request ) {
        var model = new Employee
        {
            Id = request.Id,
            EmployeeNumber = request.EmployeeNumber,
            Name = request.Name,
            WorkEmail = request.WorkEmail,
            WorkPhone = request.WorkPhone,
            DateOfHire = request.DateOfHire,
            NationalId = request.NationalId,
            Status = request.Status,
        };
        return model;
    }

}
