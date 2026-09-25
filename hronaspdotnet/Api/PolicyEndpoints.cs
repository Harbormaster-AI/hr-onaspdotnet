
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class PolicyEndpoints
{
    public static IEndpointRouteBuilder MapPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/policy").WithTags("Policys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToAcknowledgements", AddToAcknowledgements);
    group.MapPut("/removeFromAcknowledgements", RemoveFromAcknowledgements);


        return app;
    }

    private static async Task<IResult> Create(
        PolicyRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicy( request );

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
        PolicyRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicy( request );

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
        IPolicyService service,
        CancellationToken cancellationToken) {

        var policy = await service.Get(identifier, cancellationToken);
        return policy is null ? Results.NotFound() : Results.Ok( policy );
    }


    private static async Task<IResult> GetAll(
        IPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAcknowledgements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAcknowledgements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAcknowledgements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAcknowledgements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Policy mapRequestToPolicy( PolicyRequest request ) {
        var model = new Policy
        {
            Id = request.Id,
            PolicyNumber = request.PolicyNumber,
            Name = request.Name,
            EffectiveDate = request.EffectiveDate,
            Description = request.Description,
        };
        return model;
    }

}
