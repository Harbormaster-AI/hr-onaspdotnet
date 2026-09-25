
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PayrollCalendar
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PayrollcalendarId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<PayrollRun> PayrollRuns { get; set; } = new List<PayrollRun>();
public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
 public virtual PayFrequency? PayFrequency { get; set; } 

    public static PayrollCalendar FromRequest(PayrollCalendarRequest request) {
        return new PayrollCalendar {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            PayFrequency = request.PayFrequency,
        };
    }
}
