
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class JobRequisitionEndpoints
{
    public static IEndpointRouteBuilder MapJobRequisitionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/jobRequisition").WithTags("JobRequisitions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDepartment", AssignDepartment);
        group.MapPut("/unassignDepartment", UnassignDepartment);
        group.MapPut("/assignHiringManager", AssignHiringManager);
        group.MapPut("/unassignHiringManager", UnassignHiringManager);
        group.MapPut("/assignRecruiter", AssignRecruiter);
        group.MapPut("/unassignRecruiter", UnassignRecruiter);
        group.MapPut("/assignJobProfile", AssignJobProfile);
        group.MapPut("/unassignJobProfile", UnassignJobProfile);

    group.MapPut("/addToCandidates", AddToCandidates);
    group.MapPut("/removeFromCandidates", RemoveFromCandidates);

    group.MapPut("/addToInterviews", AddToInterviews);
    group.MapPut("/removeFromInterviews", RemoveFromInterviews);

    group.MapPut("/addToOffers", AddToOffers);
    group.MapPut("/removeFromOffers", RemoveFromOffers);


        return app;
    }

    private static async Task<IResult> Create(
        JobRequisitionRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobRequisition( request );

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
        JobRequisitionRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobRequisition( request );

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
        IJobRequisitionService service,
        CancellationToken cancellationToken) {

        var jobRequisition = await service.Get(identifier, cancellationToken);
        return jobRequisition is null ? Results.NotFound() : Results.Ok( jobRequisition );
    }


    private static async Task<IResult> GetAll(
        IJobRequisitionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( JobRequisitionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDepartment(
        AssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDepartment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDepartment(
    AssociationRequest request,
    IJobRequisitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDepartment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignHiringManager(
        AssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignHiringManager(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignHiringManager(
    AssociationRequest request,
    IJobRequisitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignHiringManager(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRecruiter(
        AssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRecruiter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRecruiter(
    AssociationRequest request,
    IJobRequisitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRecruiter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignJobProfile(
        AssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignJobProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignJobProfile(
    AssociationRequest request,
    IJobRequisitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignJobProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCandidates(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCandidates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCandidates(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCandidates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInterviews(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInterviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInterviews(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInterviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOffers(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOffers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOffers(
        MultipleAssociationRequest request,
        IJobRequisitionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOffers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static JobRequisition mapRequestToJobRequisition( JobRequisitionRequest request ) {
        var model = new JobRequisition
        {
            Id = request.Id,
            RequisitionNumber = request.RequisitionNumber,
            Title = request.Title,
            Openings = request.Openings,
            TargetStartDate = request.TargetStartDate,
            Status = request.Status,
            Priority = request.Priority,
        };
        return model;
    }

}
