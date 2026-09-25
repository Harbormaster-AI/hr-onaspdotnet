
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PaymentMethod
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentmethodId { get; set; } 
 public virtual bool? Preferred { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual BankAccount? BankAccount { get; set; } 
 public virtual PaymentMethodType? MethodType { get; set; } 

    public static PaymentMethod FromRequest(PaymentMethodRequest request) {
        return new PaymentMethod {
            Id = request.Id,
            Preferred = request.Preferred,
            MethodType = request.MethodType,
        };
    }
}
