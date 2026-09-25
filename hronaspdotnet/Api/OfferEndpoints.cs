
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class OfferEndpoints
{
    public static IEndpointRouteBuilder MapOfferEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/offer").WithTags("Offers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRequisition", AssignRequisition);
        group.MapPut("/unassignRequisition", UnassignRequisition);
        group.MapPut("/assignCandidate", AssignCandidate);
        group.MapPut("/unassignCandidate", UnassignCandidate);
        group.MapPut("/assignApprovedBy", AssignApprovedBy);
        group.MapPut("/unassignApprovedBy", UnassignApprovedBy);
        group.MapPut("/assignContract", AssignContract);
        group.MapPut("/unassignContract", UnassignContract);


        return app;
    }

    private static async Task<IResult> Create(
        OfferRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOffer( request );

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
        OfferRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOffer( request );

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
        IOfferService service,
        CancellationToken cancellationToken) {

        var offer = await service.Get(identifier, cancellationToken);
        return offer is null ? Results.NotFound() : Results.Ok( offer );
    }


    private static async Task<IResult> GetAll(
        IOfferService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OfferResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOfferService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRequisition(
        AssociationRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRequisition(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRequisition(
    AssociationRequest request,
    IOfferService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRequisition(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCandidate(
        AssociationRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCandidate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCandidate(
    AssociationRequest request,
    IOfferService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCandidate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApprovedBy(
        AssociationRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApprovedBy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApprovedBy(
    AssociationRequest request,
    IOfferService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApprovedBy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignContract(
        AssociationRequest request,
        IOfferService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignContract(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignContract(
    AssociationRequest request,
    IOfferService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignContract(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Offer mapRequestToOffer( OfferRequest request ) {
        var model = new Offer
        {
            Id = request.Id,
            OfferNumber = request.OfferNumber,
            ProposedStartDate = request.ProposedStartDate,
            BaseSalary = request.BaseSalary,
            SignOnBonus = request.SignOnBonus,
            Status = request.Status,
        };
        return model;
    }

}
