
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class ScheduleException
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ScheduleexceptionId { get; set; } 
 public virtual DateOnly? Date { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual decimal? Hours { get; set; } 
public virtual WorkSchedule? WorkSchedule { get; set; } 
public virtual Employee? Employee { get; set; } 

    public static ScheduleException FromRequest(ScheduleExceptionRequest request) {
        return new ScheduleException {
            Id = request.Id,
            Date = request.Date,
            Reason = request.Reason,
            Hours = request.Hours,
        };
    }
}
