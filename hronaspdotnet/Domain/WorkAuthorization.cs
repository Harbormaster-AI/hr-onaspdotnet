
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class WorkAuthorization
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkauthorizationId { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
 public virtual WorkAuthorizationStatus? Status { get; set; } 

    public static WorkAuthorization FromRequest(WorkAuthorizationRequest request) {
        return new WorkAuthorization {
            Id = request.Id,
            Country = request.Country,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
    }
}
