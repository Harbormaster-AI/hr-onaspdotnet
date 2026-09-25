
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PayrollCalendarEndpoints
{
    public static IEndpointRouteBuilder MapPayrollCalendarEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payrollCalendar").WithTags("PayrollCalendars");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToPayrollRuns", AddToPayrollRuns);
    group.MapPut("/removeFromPayrollRuns", RemoveFromPayrollRuns);

    group.MapPut("/addToEmployees", AddToEmployees);
    group.MapPut("/removeFromEmployees", RemoveFromEmployees);


        return app;
    }

    private static async Task<IResult> Create(
        PayrollCalendarRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollCalendar( request );

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
        PayrollCalendarRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayrollCalendar( request );

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
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {

        var payrollCalendar = await service.Get(identifier, cancellationToken);
        return payrollCalendar is null ? Results.NotFound() : Results.Ok( payrollCalendar );
    }


    private static async Task<IResult> GetAll(
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PayrollCalendarResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IPayrollCalendarService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPayrollRuns(
        MultipleAssociationRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayrollRuns(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayrollRuns(
        MultipleAssociationRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayrollRuns(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEmployees(
        MultipleAssociationRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEmployees(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEmployees(
        MultipleAssociationRequest request,
        IPayrollCalendarService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEmployees(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PayrollCalendar mapRequestToPayrollCalendar( PayrollCalendarRequest request ) {
        var model = new PayrollCalendar
        {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            PayFrequency = request.PayFrequency,
        };
        return model;
    }

}
