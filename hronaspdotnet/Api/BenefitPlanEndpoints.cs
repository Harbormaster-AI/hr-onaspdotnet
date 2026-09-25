
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class BenefitPlanEndpoints
{
    public static IEndpointRouteBuilder MapBenefitPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/benefitPlan").WithTags("BenefitPlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrganization", AssignOrganization);
        group.MapPut("/unassignOrganization", UnassignOrganization);

    group.MapPut("/addToEnrollments", AddToEnrollments);
    group.MapPut("/removeFromEnrollments", RemoveFromEnrollments);


        return app;
    }

    private static async Task<IResult> Create(
        BenefitPlanRequest request,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBenefitPlan( request );

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
        BenefitPlanRequest request,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBenefitPlan( request );

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
        IBenefitPlanService service,
        CancellationToken cancellationToken) {

        var benefitPlan = await service.Get(identifier, cancellationToken);
        return benefitPlan is null ? Results.NotFound() : Results.Ok( benefitPlan );
    }


    private static async Task<IResult> GetAll(
        IBenefitPlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BenefitPlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrganization(
        AssociationRequest request,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrganization(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrganization(
    AssociationRequest request,
    IBenefitPlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrganization(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToEnrollments(
        MultipleAssociationRequest request,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEnrollments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEnrollments(
        MultipleAssociationRequest request,
        IBenefitPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEnrollments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BenefitPlan mapRequestToBenefitPlan( BenefitPlanRequest request ) {
        var model = new BenefitPlan
        {
            Id = request.Id,
            Name = request.Name,
            ProviderName = request.ProviderName,
            EmployeeContributionRate = request.EmployeeContributionRate,
            EmployerContributionRate = request.EmployerContributionRate,
            EligibilityRules = request.EligibilityRules,
            BenefitType = request.BenefitType,
        };
        return model;
    }

}
