
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class WorkShift
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WorkshiftId { get; set; } 
 public virtual LocalTime? StartTime { get; set; } 
 public virtual LocalTime? EndTime { get; set; } 
 public virtual int? BreakMinutes { get; set; } 
public virtual WorkSchedule? WorkSchedule { get; set; } 
 public virtual DayOfWeek_? DayOfWeek_ { get; set; } 

    public static WorkShift FromRequest(WorkShiftRequest request) {
        return new WorkShift {
            Id = request.Id,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            BreakMinutes = request.BreakMinutes,
            DayOfWeek_ = request.DayOfWeek_,
        };
    }
}
