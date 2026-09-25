
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class BenefitPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BenefitplanId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ProviderName { get; set; } 
 public virtual Percentage? EmployeeContributionRate { get; set; } 
 public virtual Percentage? EmployerContributionRate { get; set; } 
 public virtual string? EligibilityRules { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<BenefitEnrollment> Enrollments { get; set; } = new List<BenefitEnrollment>();
 public virtual BenefitType? BenefitType { get; set; } 

    public static BenefitPlan FromRequest(BenefitPlanRequest request) {
        return new BenefitPlan {
            Id = request.Id,
            Name = request.Name,
            ProviderName = request.ProviderName,
            EmployeeContributionRate = request.EmployeeContributionRate,
            EmployerContributionRate = request.EmployerContributionRate,
            EligibilityRules = request.EligibilityRules,
            BenefitType = request.BenefitType,
        };
    }
}
