
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class EmploymentContractEndpoints
{
    public static IEndpointRouteBuilder MapEmploymentContractEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/employmentContract").WithTags("EmploymentContracts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignCompensationPackage", AssignCompensationPackage);
        group.MapPut("/unassignCompensationPackage", UnassignCompensationPackage);
        group.MapPut("/assignWorkSchedule", AssignWorkSchedule);
        group.MapPut("/unassignWorkSchedule", UnassignWorkSchedule);
        group.MapPut("/assignLocation", AssignLocation);
        group.MapPut("/unassignLocation", UnassignLocation);
        group.MapPut("/assignPayrollCalendar", AssignPayrollCalendar);
        group.MapPut("/unassignPayrollCalendar", UnassignPayrollCalendar);


        return app;
    }

    private static async Task<IResult> Create(
        EmploymentContractRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmploymentContract( request );

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
        EmploymentContractRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEmploymentContract( request );

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
        IEmploymentContractService service,
        CancellationToken cancellationToken) {

        var employmentContract = await service.Get(identifier, cancellationToken);
        return employmentContract is null ? Results.NotFound() : Results.Ok( employmentContract );
    }


    private static async Task<IResult> GetAll(
        IEmploymentContractService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EmploymentContractResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IEmploymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCompensationPackage(
        AssociationRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCompensationPackage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCompensationPackage(
    AssociationRequest request,
    IEmploymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCompensationPackage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWorkSchedule(
        AssociationRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWorkSchedule(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWorkSchedule(
    AssociationRequest request,
    IEmploymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWorkSchedule(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLocation(
        AssociationRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLocation(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLocation(
    AssociationRequest request,
    IEmploymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLocation(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayrollCalendar(
        AssociationRequest request,
        IEmploymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayrollCalendar(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayrollCalendar(
    AssociationRequest request,
    IEmploymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayrollCalendar(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static EmploymentContract mapRequestToEmploymentContract( EmploymentContractRequest request ) {
        var model = new EmploymentContract
        {
            Id = request.Id,
            ContractNumber = request.ContractNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            WorkHoursPerWeek = request.WorkHoursPerWeek,
            EmploymentType = request.EmploymentType,
            Status = request.Status,
            PayFrequency = request.PayFrequency,
        };
        return model;
    }

}
