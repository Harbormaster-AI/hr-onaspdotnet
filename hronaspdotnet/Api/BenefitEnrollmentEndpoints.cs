
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class BenefitEnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapBenefitEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/benefitEnrollment").WithTags("BenefitEnrollments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBenefitPlan", AssignBenefitPlan);
        group.MapPut("/unassignBenefitPlan", UnassignBenefitPlan);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);

    group.MapPut("/addToDependents", AddToDependents);
    group.MapPut("/removeFromDependents", RemoveFromDependents);


        return app;
    }

    private static async Task<IResult> Create(
        BenefitEnrollmentRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBenefitEnrollment( request );

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
        BenefitEnrollmentRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBenefitEnrollment( request );

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
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {

        var benefitEnrollment = await service.Get(identifier, cancellationToken);
        return benefitEnrollment is null ? Results.NotFound() : Results.Ok( benefitEnrollment );
    }


    private static async Task<IResult> GetAll(
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BenefitEnrollmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBenefitPlan(
        AssociationRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBenefitPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBenefitPlan(
    AssociationRequest request,
    IBenefitEnrollmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBenefitPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IBenefitEnrollmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDependents(
        MultipleAssociationRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDependents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDependents(
        MultipleAssociationRequest request,
        IBenefitEnrollmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDependents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BenefitEnrollment mapRequestToBenefitEnrollment( BenefitEnrollmentRequest request ) {
        var model = new BenefitEnrollment
        {
            Id = request.Id,
            EnrollmentId = request.EnrollmentId,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
            Status = request.Status,
            CoverageLevel = request.CoverageLevel,
        };
        return model;
    }

}
