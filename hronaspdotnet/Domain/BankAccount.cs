
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class BankAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BankaccountId { get; set; } 
 public virtual string? AccountHolder { get; set; } 
 public virtual string? BankName { get; set; } 
 public virtual string? Iban { get; set; } 
 public virtual string? Bic { get; set; } 
 public virtual string? AccountNumber { get; set; } 
 public virtual string? RoutingNumber { get; set; } 

    public static BankAccount FromRequest(BankAccountRequest request) {
        return new BankAccount {
            Id = request.Id,
            AccountHolder = request.AccountHolder,
            BankName = request.BankName,
            Iban = request.Iban,
            Bic = request.Bic,
            AccountNumber = request.AccountNumber,
            RoutingNumber = request.RoutingNumber,
        };
    }
}
