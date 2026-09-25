
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class EmploymentAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EmploymentassignmentId { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual bool? Primary { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual Position? Position { get; set; } 
public virtual Employee? Supervisor { get; set; } 
 public virtual AssignmentType? AssignmentType { get; set; } 
 public virtual AssignmentStatus? Status { get; set; } 

    public static EmploymentAssignment FromRequest(EmploymentAssignmentRequest request) {
        return new EmploymentAssignment {
            Id = request.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Primary = request.Primary,
            AssignmentType = request.AssignmentType,
            Status = request.Status,
        };
    }
}
