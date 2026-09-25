
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class CompensationPackage
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompensationpackageId { get; set; } 
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
 public virtual string? Currency { get; set; } 
public virtual EmploymentContract? Contract { get; set; } 
public virtual ICollection<SalaryComponent> SalaryComponents { get; set; } = new List<SalaryComponent>();
public virtual ICollection<BonusPlan> BonusPlans { get; set; } = new List<BonusPlan>();
public virtual ICollection<EquityGrant> EquityGrants { get; set; } = new List<EquityGrant>();

    public static CompensationPackage FromRequest(CompensationPackageRequest request) {
        return new CompensationPackage {
            Id = request.Id,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
            Currency = request.Currency,
        };
    }
}
