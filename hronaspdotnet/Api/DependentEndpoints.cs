
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class DependentEndpoints
{
    public static IEndpointRouteBuilder MapDependentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dependent").WithTags("Dependents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBenefitEnrollment", AssignBenefitEnrollment);
        group.MapPut("/unassignBenefitEnrollment", UnassignBenefitEnrollment);
        group.MapPut("/assignEmployee", AssignEmployee);
        group.MapPut("/unassignEmployee", UnassignEmployee);


        return app;
    }

    private static async Task<IResult> Create(
        DependentRequest request,
        IDependentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDependent( request );

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
        DependentRequest request,
        IDependentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDependent( request );

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
        IDependentService service,
        CancellationToken cancellationToken) {

        var dependent = await service.Get(identifier, cancellationToken);
        return dependent is null ? Results.NotFound() : Results.Ok( dependent );
    }


    private static async Task<IResult> GetAll(
        IDependentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DependentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDependentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBenefitEnrollment(
        AssociationRequest request,
        IDependentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBenefitEnrollment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBenefitEnrollment(
    AssociationRequest request,
    IDependentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBenefitEnrollment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignEmployee(
        AssociationRequest request,
        IDependentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignEmployee(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignEmployee(
    AssociationRequest request,
    IDependentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignEmployee(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Dependent mapRequestToDependent( DependentRequest request ) {
        var model = new Dependent
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Relationship = request.Relationship,
        };
        return model;
    }

}
