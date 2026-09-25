
using hronaspdotnet.Service;
using hronaspdotnet.Domain;
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Api;

public static class OrganizationEndpoints
{
    public static IEndpointRouteBuilder MapOrganizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/organization").WithTags("Organizations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToDepartments", AddToDepartments);
    group.MapPut("/removeFromDepartments", RemoveFromDepartments);

    group.MapPut("/addToLocations", AddToLocations);
    group.MapPut("/removeFromLocations", RemoveFromLocations);

    group.MapPut("/addToJobFamilies", AddToJobFamilies);
    group.MapPut("/removeFromJobFamilies", RemoveFromJobFamilies);

    group.MapPut("/addToBenefitPlans", AddToBenefitPlans);
    group.MapPut("/removeFromBenefitPlans", RemoveFromBenefitPlans);

    group.MapPut("/addToCostCenters", AddToCostCenters);
    group.MapPut("/removeFromCostCenters", RemoveFromCostCenters);

    group.MapPut("/addToPayrollCalendars", AddToPayrollCalendars);
    group.MapPut("/removeFromPayrollCalendars", RemoveFromPayrollCalendars);


        return app;
    }

    private static async Task<IResult> Create(
        OrganizationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrganization( request );

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
        OrganizationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrganization( request );

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
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var organization = await service.Get(identifier, cancellationToken);
        return organization is null ? Results.NotFound() : Results.Ok( organization );
    }


    private static async Task<IResult> GetAll(
        IOrganizationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OrganizationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDepartments(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDepartments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDepartments(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDepartments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLocations(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLocations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLocations(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLocations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToJobFamilies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToJobFamilies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromJobFamilies(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromJobFamilies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBenefitPlans(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBenefitPlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBenefitPlans(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBenefitPlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCostCenters(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCostCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCostCenters(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCostCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayrollCalendars(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayrollCalendars(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayrollCalendars(
        MultipleAssociationRequest request,
        IOrganizationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayrollCalendars(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Organization mapRequestToOrganization( OrganizationRequest request ) {
        var model = new Organization
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            RegistrationCountry = request.RegistrationCountry,
            Website = request.Website,
        };
        return model;
    }

}
