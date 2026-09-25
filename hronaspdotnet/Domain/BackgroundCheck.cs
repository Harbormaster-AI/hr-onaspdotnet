
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class BackgroundCheck
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BackgroundcheckId { get; set; } 
 public virtual string? CheckNumber { get; set; } 
 public virtual string? Provider { get; set; } 
 public virtual DateOnly? CompletedDate { get; set; } 
public virtual Candidate? Candidate { get; set; } 
public virtual JobRequisition? Requisition { get; set; } 
public virtual Document? Report { get; set; } 
 public virtual BackgroundCheckStatus? Status { get; set; } 

    public static BackgroundCheck FromRequest(BackgroundCheckRequest request) {
        return new BackgroundCheck {
            Id = request.Id,
            CheckNumber = request.CheckNumber,
            Provider = request.Provider,
            CompletedDate = request.CompletedDate,
            Status = request.Status,
        };
    }
}
