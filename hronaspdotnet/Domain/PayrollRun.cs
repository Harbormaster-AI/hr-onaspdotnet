
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PayrollRun
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PayrollrunId { get; set; } 
 public virtual string? RunNumber { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
public virtual PayrollCalendar? PayrollCalendar { get; set; } 
public virtual ICollection<PayrollItem> PayrollItems { get; set; } = new List<PayrollItem>();
 public virtual PayrollStatus? Status { get; set; } 

    public static PayrollRun FromRequest(PayrollRunRequest request) {
        return new PayrollRun {
            Id = request.Id,
            RunNumber = request.RunNumber,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            PaymentDate = request.PaymentDate,
            Status = request.Status,
        };
    }
}
