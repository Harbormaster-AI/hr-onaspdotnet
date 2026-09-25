
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class BonusPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BonusplanId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Percentage? TargetPercentage { get; set; } 
public virtual ICollection<CompensationPackage> CompensationPackages { get; set; } = new List<CompensationPackage>();

    public static BonusPlan FromRequest(BonusPlanRequest request) {
        return new BonusPlan {
            Id = request.Id,
            Name = request.Name,
            TargetPercentage = request.TargetPercentage,
        };
    }
}
