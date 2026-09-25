
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class LeavePolicyEndpoints
{
    public static IEndpointRouteBuilder MapLeavePolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/leavePolicy").WithTags("LeavePolicys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToLeaveRequests", AddToLeaveRequests);
    group.MapPut("/removeFromLeaveRequests", RemoveFromLeaveRequests);


        return app;
    }

    private static async Task<IResult> Create(
        LeavePolicyRequest request,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLeavePolicy( request );

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
        LeavePolicyRequest request,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLeavePolicy( request );

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
        ILeavePolicyService service,
        CancellationToken cancellationToken) {

        var leavePolicy = await service.Get(identifier, cancellationToken);
        return leavePolicy is null ? Results.NotFound() : Results.Ok( leavePolicy );
    }


    private static async Task<IResult> GetAll(
        ILeavePolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LeavePolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    ILeavePolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToLeaveRequests(
        MultipleAssociationRequest request,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLeaveRequests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLeaveRequests(
        MultipleAssociationRequest request,
        ILeavePolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLeaveRequests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static LeavePolicy mapRequestToLeavePolicy( LeavePolicyRequest request ) {
        var model = new LeavePolicy
        {
            Id = request.Id,
            Name = request.Name,
            AccrualRate = request.AccrualRate,
            CarryoverAllowed = request.CarryoverAllowed,
            MaxBalance = request.MaxBalance,
            LeaveCategory = request.LeaveCategory,
            AccrualUnit = request.AccrualUnit,
        };
        return model;
    }

}
