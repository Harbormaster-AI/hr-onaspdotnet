
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class TerminationEndpoints
{
    public static IEndpointRouteBuilder MapTerminationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/termination").WithTags("Terminations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);
        group.MapPut("/assignAssignment", AssignAssignment);
        group.MapPut("/unassignAssignment", UnassignAssignment);


        return app;
    }

    private static async Task<IResult> Create(
        TerminationRequest request,
        ITerminationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTermination( request );

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
        TerminationRequest request,
        ITerminationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTermination( request );

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
        ITerminationService service,
        CancellationToken cancellationToken) {

        var termination = await service.Get(identifier, cancellationToken);
        return termination is null ? Results.NotFound() : Results.Ok( termination );
    }


    private static async Task<IResult> GetAll(
        ITerminationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TerminationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITerminationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        ITerminationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    ITerminationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAssignment(
        AssociationRequest request,
        ITerminationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAssignment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAssignment(
    AssociationRequest request,
    ITerminationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAssignment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Termination mapRequestToTermination( TerminationRequest request ) {
        var model = new Termination
        {
            Id = request.Id,
            TerminationNumber = request.TerminationNumber,
            TerminationDate = request.TerminationDate,
            Notes = request.Notes,
            EligibleForRehire = request.EligibleForRehire,
            Reason = request.Reason,
            Type = request.Type,
        };
        return model;
    }

}
