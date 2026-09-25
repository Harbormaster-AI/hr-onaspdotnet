
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class OnboardingTask
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OnboardingtaskId { get; set; } 
 public virtual string? TaskNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual Employee? AssignedTo { get; set; } 
public virtual ICollection<OnboardingTask> Dependencies { get; set; } = new List<OnboardingTask>();
public virtual Offer? RelatedOffer { get; set; } 
 public virtual OnboardingTaskStatus? Status { get; set; } 

    public static OnboardingTask FromRequest(OnboardingTaskRequest request) {
        return new OnboardingTask {
            Id = request.Id,
            TaskNumber = request.TaskNumber,
            Name = request.Name,
            DueDate = request.DueDate,
            Status = request.Status,
        };
    }
}
