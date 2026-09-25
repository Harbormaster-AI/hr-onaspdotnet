
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class TaxWithholding
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TaxwithholdingId { get; set; } 
 public virtual TaxId? TaxId { get; set; } 
 public virtual int? Allowances { get; set; } 
 public virtual Money? AdditionalAmount { get; set; } 
public virtual Employee? Employee { get; set; } 
 public virtual FilingStatus? FilingStatus { get; set; } 

    public static TaxWithholding FromRequest(TaxWithholdingRequest request) {
        return new TaxWithholding {
            Id = request.Id,
            TaxId = request.TaxId,
            Allowances = request.Allowances,
            AdditionalAmount = request.AdditionalAmount,
            FilingStatus = request.FilingStatus,
        };
    }
}
