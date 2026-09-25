
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PolicyAcknowledgement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PolicyacknowledgementId { get; set; } 
 public virtual DateOnly? AcknowledgementDate { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual Employee? Employee { get; set; } 
 public virtual AcknowledgementStatus? Status { get; set; } 

    public static PolicyAcknowledgement FromRequest(PolicyAcknowledgementRequest request) {
        return new PolicyAcknowledgement {
            Id = request.Id,
            AcknowledgementDate = request.AcknowledgementDate,
            Status = request.Status,
        };
    }
}
