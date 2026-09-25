
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Organization
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OrganizationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? RegistrationCountry { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
public virtual ICollection<JobFamily> JobFamilies { get; set; } = new List<JobFamily>();
public virtual ICollection<BenefitPlan> BenefitPlans { get; set; } = new List<BenefitPlan>();
public virtual ICollection<CostCenter> CostCenters { get; set; } = new List<CostCenter>();
public virtual ICollection<PayrollCalendar> PayrollCalendars { get; set; } = new List<PayrollCalendar>();

    public static Organization FromRequest(OrganizationRequest request) {
        return new Organization {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            RegistrationCountry = request.RegistrationCountry,
            Website = request.Website,
        };
    }
}
