
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class JobFamilyEndpoints
{
    public static IEndpointRouteBuilder MapJobFamilyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/jobFamily").WithTags("JobFamilys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToJobProfiles", AddToJobProfiles);
    group.MapPut("/removeFromJobProfiles", RemoveFromJobProfiles);


        return app;
    }

    private static async Task<IResult> Create(
        JobFamilyRequest request,
        IJobFamilyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobFamily( request );

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
        JobFamilyRequest request,
        IJobFamilyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToJobFamily( request );

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
        IJobFamilyService service,
        CancellationToken cancellationToken) {

        var jobFamily = await service.Get(identifier, cancellationToken);
        return jobFamily is null ? Results.NotFound() : Results.Ok( jobFamily );
    }


    private static async Task<IResult> GetAll(
        IJobFamilyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( JobFamilyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IJobFamilyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IJobFamilyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IJobFamilyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToJobProfiles(
        MultipleAssociationRequest request,
        IJobFamilyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToJobProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromJobProfiles(
        MultipleAssociationRequest request,
        IJobFamilyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromJobProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static JobFamily mapRequestToJobFamily( JobFamilyRequest request ) {
        var model = new JobFamily
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };
        return model;
    }

}
