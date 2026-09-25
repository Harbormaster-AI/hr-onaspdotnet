
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class EmploymentContract
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EmploymentcontractId { get; set; } 
 public virtual string? ContractNumber { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual decimal? WorkHoursPerWeek { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual CompensationPackage? CompensationPackage { get; set; } 
public virtual WorkSchedule? WorkSchedule { get; set; } 
public virtual Location? Location { get; set; } 
public virtual PayrollCalendar? PayrollCalendar { get; set; } 
 public virtual EmploymentType? EmploymentType { get; set; } 
 public virtual ContractStatus? Status { get; set; } 
 public virtual PayFrequency? PayFrequency { get; set; } 

    public static EmploymentContract FromRequest(EmploymentContractRequest request) {
        return new EmploymentContract {
            Id = request.Id,
            ContractNumber = request.ContractNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            WorkHoursPerWeek = request.WorkHoursPerWeek,
            EmploymentType = request.EmploymentType,
            Status = request.Status,
            PayFrequency = request.PayFrequency,
        };
    }
}
