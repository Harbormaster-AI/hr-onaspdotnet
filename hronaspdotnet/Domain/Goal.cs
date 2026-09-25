
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Goal
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GoalId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? TargetDate { get; set; } 
 public virtual Percentage? Weight { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual PerformanceCycle? Cycle { get; set; } 
public virtual Goal? ParentGoal { get; set; } 
public virtual ICollection<Goal> ChildGoals { get; set; } = new List<Goal>();
 public virtual GoalStatus? Status { get; set; } 

    public static Goal FromRequest(GoalRequest request) {
        return new Goal {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            TargetDate = request.TargetDate,
            Weight = request.Weight,
            Status = request.Status,
        };
    }
}
