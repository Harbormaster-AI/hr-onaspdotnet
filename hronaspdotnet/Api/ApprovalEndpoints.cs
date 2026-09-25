
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class ApprovalEndpoints
{
    public static IEndpointRouteBuilder MapApprovalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/approval").WithTags("Approvals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignApprover", AssignApprover);
        group.MapPut("/unassignApprover", UnassignApprover);
        group.MapPut("/assignTimesheet", AssignTimesheet);
        group.MapPut("/unassignTimesheet", UnassignTimesheet);
        group.MapPut("/assignLeaveRequest", AssignLeaveRequest);
        group.MapPut("/unassignLeaveRequest", UnassignLeaveRequest);


        return app;
    }

    private static async Task<IResult> Create(
        ApprovalRequest request,
        IApprovalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApproval( request );

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
        ApprovalRequest request,
        IApprovalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApproval( request );

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
        IApprovalService service,
        CancellationToken cancellationToken) {

        var approval = await service.Get(identifier, cancellationToken);
        return approval is null ? Results.NotFound() : Results.Ok( approval );
    }


    private static async Task<IResult> GetAll(
        IApprovalService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ApprovalResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IApprovalService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApprover(
        AssociationRequest request,
        IApprovalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApprover(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApprover(
    AssociationRequest request,
    IApprovalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApprover(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTimesheet(
        AssociationRequest request,
        IApprovalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTimesheet(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTimesheet(
    AssociationRequest request,
    IApprovalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTimesheet(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLeaveRequest(
        AssociationRequest request,
        IApprovalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLeaveRequest(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLeaveRequest(
    AssociationRequest request,
    IApprovalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLeaveRequest(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Approval mapRequestToApproval( ApprovalRequest request ) {
        var model = new Approval
        {
            Id = request.Id,
            ApproverComment = request.ApproverComment,
            ActionDate = request.ActionDate,
            Status = request.Status,
        };
        return model;
    }

}
