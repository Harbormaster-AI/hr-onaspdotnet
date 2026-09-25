
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class LeaveRequestEndpoints
{
    public static IEndpointRouteBuilder MapLeaveRequestEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/leaveRequest").WithTags("LeaveRequests");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignLeavePolicy", AssignLeavePolicy);
        group.MapPut("/unassignLeavePolicy", UnassignLeavePolicy);

    group.MapPut("/addToApprovals", AddToApprovals);
    group.MapPut("/removeFromApprovals", RemoveFromApprovals);


        return app;
    }

    private static async Task<IResult> Create(
        LeaveRequestRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLeaveRequest( request );

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
        LeaveRequestRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLeaveRequest( request );

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
        ILeaveRequestService service,
        CancellationToken cancellationToken) {

        var leaveRequest = await service.Get(identifier, cancellationToken);
        return leaveRequest is null ? Results.NotFound() : Results.Ok( leaveRequest );
    }


    private static async Task<IResult> GetAll(
        ILeaveRequestService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LeaveRequestResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ILeaveRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLeavePolicy(
        AssociationRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLeavePolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLeavePolicy(
    AssociationRequest request,
    ILeaveRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLeavePolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToApprovals(
        MultipleAssociationRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApprovals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApprovals(
        MultipleAssociationRequest request,
        ILeaveRequestService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApprovals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LeaveRequest mapRequestToLeaveRequest( LeaveRequestRequest request ) {
        var model = new LeaveRequest
        {
            Id = request.Id,
            RequestNumber = request.RequestNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason,
            Hours = request.Hours,
            Status = request.Status,
        };
        return model;
    }

}
