
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class WorkSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkscheduleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual decimal? StandardHoursPerWeek { get; set; } 
public virtual ICollection<EmploymentContract> Contracts { get; set; } = new List<EmploymentContract>();
public virtual ICollection<WorkShift> Shifts { get; set; } = new List<WorkShift>();
public virtual ICollection<ScheduleException> Exceptions { get; set; } = new List<ScheduleException>();
 public virtual ScheduleType? ScheduleType { get; set; } 

    public static WorkSchedule FromRequest(WorkScheduleRequest request) {
        return new WorkSchedule {
            Id = request.Id,
            Name = request.Name,
            StandardHoursPerWeek = request.StandardHoursPerWeek,
            ScheduleType = request.ScheduleType,
        };
    }
}
