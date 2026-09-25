
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class WorkAuthorizationEndpoints
{
    public static IEndpointRouteBuilder MapWorkAuthorizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/workAuthorization").WithTags("WorkAuthorizations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);

    group.MapPut("/addToDocuments", AddToDocuments);
    group.MapPut("/removeFromDocuments", RemoveFromDocuments);


        return app;
    }

    private static async Task<IResult> Create(
        WorkAuthorizationRequest request,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkAuthorization( request );

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
        WorkAuthorizationRequest request,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWorkAuthorization( request );

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
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {

        var workAuthorization = await service.Get(identifier, cancellationToken);
        return workAuthorization is null ? Results.NotFound() : Results.Ok( workAuthorization );
    }


    private static async Task<IResult> GetAll(
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WorkAuthorizationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IWorkAuthorizationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDocuments(
        MultipleAssociationRequest request,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDocuments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDocuments(
        MultipleAssociationRequest request,
        IWorkAuthorizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDocuments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static WorkAuthorization mapRequestToWorkAuthorization( WorkAuthorizationRequest request ) {
        var model = new WorkAuthorization
        {
            Id = request.Id,
            Country = request.Country,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
        return model;
    }

}
