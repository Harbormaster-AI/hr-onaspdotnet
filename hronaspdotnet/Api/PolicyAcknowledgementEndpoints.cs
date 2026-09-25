
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PolicyAcknowledgementEndpoints
{
    public static IEndpointRouteBuilder MapPolicyAcknowledgementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/policyAcknowledgement").WithTags("PolicyAcknowledgements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        PolicyAcknowledgementRequest request,
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicyAcknowledgement( request );

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
        PolicyAcknowledgementRequest request,
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicyAcknowledgement( request );

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
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {

        var policyAcknowledgement = await service.Get(identifier, cancellationToken);
        return policyAcknowledgement is null ? Results.NotFound() : Results.Ok( policyAcknowledgement );
    }


    private static async Task<IResult> GetAll(
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PolicyAcknowledgementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IPolicyAcknowledgementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IPolicyAcknowledgementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IPolicyAcknowledgementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PolicyAcknowledgement mapRequestToPolicyAcknowledgement( PolicyAcknowledgementRequest request ) {
        var model = new PolicyAcknowledgement
        {
            Id = request.Id,
            AcknowledgementDate = request.AcknowledgementDate,
            Status = request.Status,
        };
        return model;
    }

}
