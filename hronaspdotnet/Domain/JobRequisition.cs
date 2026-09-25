
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class JobRequisition
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? JobrequisitionId { get; set; } 
 public virtual string? RequisitionNumber { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual int? Openings { get; set; } 
 public virtual DateOnly? TargetStartDate { get; set; } 
public virtual Department? Department { get; set; } 
public virtual Employee? HiringManager { get; set; } 
public virtual Employee? Recruiter { get; set; } 
public virtual JobProfile? JobProfile { get; set; } 
public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
 public virtual RequisitionStatus? Status { get; set; } 
 public virtual RequisitionPriority? Priority { get; set; } 

    public static JobRequisition FromRequest(JobRequisitionRequest request) {
        return new JobRequisition {
            Id = request.Id,
            RequisitionNumber = request.RequisitionNumber,
            Title = request.Title,
            Openings = request.Openings,
            TargetStartDate = request.TargetStartDate,
            Status = request.Status,
            Priority = request.Priority,
        };
    }
}
