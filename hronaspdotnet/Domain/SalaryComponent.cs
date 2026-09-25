
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class SalaryComponent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SalarycomponentId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual bool? Recurring { get; set; } 
public virtual CompensationPackage? CompensationPackage { get; set; } 
 public virtual SalaryComponentType? ComponentType { get; set; } 

    public static SalaryComponent FromRequest(SalaryComponentRequest request) {
        return new SalaryComponent {
            Id = request.Id,
            Amount = request.Amount,
            Recurring = request.Recurring,
            ComponentType = request.ComponentType,
        };
    }
}
