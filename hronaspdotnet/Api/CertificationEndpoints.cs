
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class CertificationEndpoints
{
    public static IEndpointRouteBuilder MapCertificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/certification").WithTags("Certifications");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignCourse", AssignCourse);
        group.MapPut("/unassignCourse", UnassignCourse);


        return app;
    }

    private static async Task<IResult> Create(
        CertificationRequest request,
        ICertificationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCertification( request );

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
        CertificationRequest request,
        ICertificationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCertification( request );

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
        ICertificationService service,
        CancellationToken cancellationToken) {

        var certification = await service.Get(identifier, cancellationToken);
        return certification is null ? Results.NotFound() : Results.Ok( certification );
    }


    private static async Task<IResult> GetAll(
        ICertificationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CertificationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICertificationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ICertificationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ICertificationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCourse(
        AssociationRequest request,
        ICertificationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCourse(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCourse(
    AssociationRequest request,
    ICertificationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCourse(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Certification mapRequestToCertification( CertificationRequest request ) {
        var model = new Certification
        {
            Id = request.Id,
            Name = request.Name,
            Issuer = request.Issuer,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo,
            CredentialId = request.CredentialId,
        };
        return model;
    }

}
