
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class LeavePolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LeavepolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual decimal? AccrualRate { get; set; } 
 public virtual bool? CarryoverAllowed { get; set; } 
 public virtual decimal? MaxBalance { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
 public virtual LeaveCategory? LeaveCategory { get; set; } 
 public virtual AccrualUnit? AccrualUnit { get; set; } 

    public static LeavePolicy FromRequest(LeavePolicyRequest request) {
        return new LeavePolicy {
            Id = request.Id,
            Name = request.Name,
            AccrualRate = request.AccrualRate,
            CarryoverAllowed = request.CarryoverAllowed,
            MaxBalance = request.MaxBalance,
            LeaveCategory = request.LeaveCategory,
            AccrualUnit = request.AccrualUnit,
        };
    }
}
