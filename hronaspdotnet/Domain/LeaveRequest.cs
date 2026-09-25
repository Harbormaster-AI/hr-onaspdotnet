
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class LeaveRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LeaverequestId { get; set; } 
 public virtual string? RequestNumber { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual decimal? Hours { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual LeavePolicy? LeavePolicy { get; set; } 
public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();
 public virtual LeaveStatus? Status { get; set; } 

    public static LeaveRequest FromRequest(LeaveRequestRequest request) {
        return new LeaveRequest {
            Id = request.Id,
            RequestNumber = request.RequestNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Reason = request.Reason,
            Hours = request.Hours,
            Status = request.Status,
        };
    }
}
