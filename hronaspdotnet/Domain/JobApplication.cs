
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class JobApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? JobapplicationId { get; set; } 
 public virtual string? ApplicationNumber { get; set; } 
 public virtual DateOnly? AppliedDate { get; set; } 
 public virtual string? ResumeUrl { get; set; } 
public virtual Candidate? Candidate { get; set; } 
public virtual JobRequisition? Requisition { get; set; } 
public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
 public virtual ApplicationStatus? Status { get; set; } 

    public static JobApplication FromRequest(JobApplicationRequest request) {
        return new JobApplication {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            AppliedDate = request.AppliedDate,
            ResumeUrl = request.ResumeUrl,
            Status = request.Status,
        };
    }
}
