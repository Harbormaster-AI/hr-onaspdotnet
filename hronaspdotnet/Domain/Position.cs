
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Position
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PositionId { get; set; } 
 public virtual string? PositionCode { get; set; } 
 public virtual decimal? Fte { get; set; } 
public virtual Department? Department { get; set; } 
public virtual JobProfile? JobProfile { get; set; } 
public virtual CostCenter? CostCenter { get; set; } 
public virtual Location? Location { get; set; } 
public virtual Position? ManagerPosition { get; set; } 
public virtual ICollection<Position> DirectReports { get; set; } = new List<Position>();
public virtual ICollection<EmploymentAssignment> Assignments { get; set; } = new List<EmploymentAssignment>();
 public virtual PositionStatus? Status { get; set; } 
 public virtual WorkLocationType? WorkLocationType { get; set; } 

    public static Position FromRequest(PositionRequest request) {
        return new Position {
            Id = request.Id,
            PositionCode = request.PositionCode,
            Fte = request.Fte,
            Status = request.Status,
            WorkLocationType = request.WorkLocationType,
        };
    }
}
