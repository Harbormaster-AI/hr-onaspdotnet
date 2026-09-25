
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PayrollItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PayrollitemId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual bool? Taxable { get; set; } 
public virtual PayrollRun? PayrollRun { get; set; } 
public virtual Employee? Employee { get; set; } 
 public virtual PayrollItemType? ItemType { get; set; } 

    public static PayrollItem FromRequest(PayrollItemRequest request) {
        return new PayrollItem {
            Id = request.Id,
            Amount = request.Amount,
            Taxable = request.Taxable,
            ItemType = request.ItemType,
        };
    }
}
