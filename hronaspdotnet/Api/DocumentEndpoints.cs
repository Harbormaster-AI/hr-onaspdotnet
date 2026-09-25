
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class DocumentEndpoints
{
    public static IEndpointRouteBuilder MapDocumentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/document").WithTags("Documents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCandidate", AssignCandidate);
        group.MapPut("/unassignCandidate", UnassignCandidate);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        DocumentRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDocument( request );

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
        DocumentRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDocument( request );

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
        IDocumentService service,
        CancellationToken cancellationToken) {

        var document = await service.Get(identifier, cancellationToken);
        return document is null ? Results.NotFound() : Results.Ok( document );
    }


    private static async Task<IResult> GetAll(
        IDocumentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DocumentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCandidate(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCandidate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCandidate(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCandidate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Document mapRequestToDocument( DocumentRequest request ) {
        var model = new Document
        {
            Id = request.Id,
            Name = request.Name,
            FileUrl = request.FileUrl,
            UploadedDate = request.UploadedDate,
            DocumentType = request.DocumentType,
        };
        return model;
    }

}
