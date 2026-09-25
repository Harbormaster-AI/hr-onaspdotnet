
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Approval
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ApprovalId { get; set; } 
 public virtual string? ApproverComment { get; set; } 
 public virtual DateOnly? ActionDate { get; set; } 
public virtual Employee? Approver { get; set; } 
public virtual Timesheet? Timesheet { get; set; } 
public virtual LeaveRequest? LeaveRequest { get; set; } 
 public virtual ApprovalStatus? Status { get; set; } 

    public static Approval FromRequest(ApprovalRequest request) {
        return new Approval {
            Id = request.Id,
            ApproverComment = request.ApproverComment,
            ActionDate = request.ActionDate,
            Status = request.Status,
        };
    }
}
