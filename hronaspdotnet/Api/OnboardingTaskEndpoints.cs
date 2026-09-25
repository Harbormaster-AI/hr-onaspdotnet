
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class OnboardingTaskEndpoints
{
    public static IEndpointRouteBuilder MapOnboardingTaskEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/onboardingTask").WithTags("OnboardingTasks");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignAssignedTo", AssignAssignedTo);
        group.MapPut("/unassignAssignedTo", UnassignAssignedTo);
        group.MapPut("/assignRelatedOffer", AssignRelatedOffer);
        group.MapPut("/unassignRelatedOffer", UnassignRelatedOffer);

    group.MapPut("/addToDependencies", AddToDependencies);
    group.MapPut("/removeFromDependencies", RemoveFromDependencies);


        return app;
    }

    private static async Task<IResult> Create(
        OnboardingTaskRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOnboardingTask( request );

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
        OnboardingTaskRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOnboardingTask( request );

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
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {

        var onboardingTask = await service.Get(identifier, cancellationToken);
        return onboardingTask is null ? Results.NotFound() : Results.Ok( onboardingTask );
    }


    private static async Task<IResult> GetAll(
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OnboardingTaskResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IOnboardingTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAssignedTo(
        AssociationRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAssignedTo(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAssignedTo(
    AssociationRequest request,
    IOnboardingTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAssignedTo(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRelatedOffer(
        AssociationRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRelatedOffer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRelatedOffer(
    AssociationRequest request,
    IOnboardingTaskService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRelatedOffer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDependencies(
        MultipleAssociationRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDependencies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDependencies(
        MultipleAssociationRequest request,
        IOnboardingTaskService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDependencies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static OnboardingTask mapRequestToOnboardingTask( OnboardingTaskRequest request ) {
        var model = new OnboardingTask
        {
            Id = request.Id,
            TaskNumber = request.TaskNumber,
            Name = request.Name,
            DueDate = request.DueDate,
            Status = request.Status,
        };
        return model;
    }

}
