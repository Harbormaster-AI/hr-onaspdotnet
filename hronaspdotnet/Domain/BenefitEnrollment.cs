
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class BenefitEnrollment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BenefitenrollmentId { get; set; } 
 public virtual string? EnrollmentId { get; set; } 
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
public virtual BenefitPlan? BenefitPlan { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
 public virtual BenefitEnrollmentStatus? Status { get; set; } 
 public virtual CoverageLevel? CoverageLevel { get; set; } 

    public static BenefitEnrollment FromRequest(BenefitEnrollmentRequest request) {
        return new BenefitEnrollment {
            Id = request.Id,
            EnrollmentId = request.EnrollmentId,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
            Status = request.Status,
            CoverageLevel = request.CoverageLevel,
        };
    }
}
