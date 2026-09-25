
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Timesheet
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TimesheetId { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual DateOnly? SubmissionDate { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();
 public virtual TimesheetStatus? Status { get; set; } 

    public static Timesheet FromRequest(TimesheetRequest request) {
        return new Timesheet {
            Id = request.Id,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            SubmissionDate = request.SubmissionDate,
            Status = request.Status,
        };
    }
}
