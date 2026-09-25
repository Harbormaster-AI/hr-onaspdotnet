
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class TimeEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TimeentryId { get; set; } 
 public virtual DateOnly? EntryDate { get; set; } 
 public virtual decimal? HoursWorked { get; set; } 
public virtual Timesheet? Timesheet { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual CostCenter? CostCenter { get; set; } 
 public virtual TimeEntryType? EntryType { get; set; } 

    public static TimeEntry FromRequest(TimeEntryRequest request) {
        return new TimeEntry {
            Id = request.Id,
            EntryDate = request.EntryDate,
            HoursWorked = request.HoursWorked,
            EntryType = request.EntryType,
        };
    }
}
