
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class GoalEndpoints
{
    public static IEndpointRouteBuilder MapGoalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/goal").WithTags("Goals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignCycle", AssignCycle);
        group.MapPut("/unassignCycle", UnassignCycle);
        group.MapPut("/assignParentGoal", AssignParentGoal);
        group.MapPut("/unassignParentGoal", UnassignParentGoal);

    group.MapPut("/addToChildGoals", AddToChildGoals);
    group.MapPut("/removeFromChildGoals", RemoveFromChildGoals);


        return app;
    }

    private static async Task<IResult> Create(
        GoalRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoal( request );

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
        GoalRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGoal( request );

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
        IGoalService service,
        CancellationToken cancellationToken) {

        var goal = await service.Get(identifier, cancellationToken);
        return goal is null ? Results.NotFound() : Results.Ok( goal );
    }


    private static async Task<IResult> GetAll(
        IGoalService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GoalResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGoalService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IGoalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCycle(
        AssociationRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCycle(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCycle(
    AssociationRequest request,
    IGoalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCycle(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignParentGoal(
        AssociationRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignParentGoal(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignParentGoal(
    AssociationRequest request,
    IGoalService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignParentGoal(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChildGoals(
        MultipleAssociationRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChildGoals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChildGoals(
        MultipleAssociationRequest request,
        IGoalService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChildGoals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Goal mapRequestToGoal( GoalRequest request ) {
        var model = new Goal
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            TargetDate = request.TargetDate,
            Weight = request.Weight,
            Status = request.Status,
        };
        return model;
    }

}
