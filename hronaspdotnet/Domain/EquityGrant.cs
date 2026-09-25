
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class EquityGrant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EquitygrantId { get; set; } 
 public virtual string? GrantId { get; set; } 
 public virtual int? GrantedUnits { get; set; } 
 public virtual DateOnly? VestingStart { get; set; } 
public virtual CompensationPackage? CompensationPackage { get; set; } 
 public virtual EquityType? GrantType { get; set; } 

    public static EquityGrant FromRequest(EquityGrantRequest request) {
        return new EquityGrant {
            Id = request.Id,
            GrantId = request.GrantId,
            GrantedUnits = request.GrantedUnits,
            VestingStart = request.VestingStart,
            GrantType = request.GrantType,
        };
    }
}
