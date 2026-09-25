
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Termination
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TerminationId { get; set; } 
 public virtual string? TerminationNumber { get; set; } 
 public virtual DateOnly? TerminationDate { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual bool? EligibleForRehire { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual EmploymentAssignment? Assignment { get; set; } 
 public virtual TerminationReason? Reason { get; set; } 
 public virtual TerminationType? Type { get; set; } 

    public static Termination FromRequest(TerminationRequest request) {
        return new Termination {
            Id = request.Id,
            TerminationNumber = request.TerminationNumber,
            TerminationDate = request.TerminationDate,
            Notes = request.Notes,
            EligibleForRehire = request.EligibleForRehire,
            Reason = request.Reason,
            Type = request.Type,
        };
    }
}
