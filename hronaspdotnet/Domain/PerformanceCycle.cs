
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PerformanceCycle
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PerformancecycleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<PerformanceReview> Reviews { get; set; } = new List<PerformanceReview>();
public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
 public virtual CycleStatus? Status { get; set; } 

    public static PerformanceCycle FromRequest(PerformanceCycleRequest request) {
        return new PerformanceCycle {
            Id = request.Id,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
    }
}
