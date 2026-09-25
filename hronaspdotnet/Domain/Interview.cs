
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Interview
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InterviewId { get; set; } 
 public virtual DateOnly? InterviewDate { get; set; } 
 public virtual string? Feedback { get; set; } 
public virtual JobRequisition? Requisition { get; set; } 
public virtual Candidate? Candidate { get; set; } 
public virtual ICollection<Employee> Interviewers { get; set; } = new List<Employee>();
 public virtual InterviewStage? Stage { get; set; } 
 public virtual InterviewResult? Result { get; set; } 

    public static Interview FromRequest(InterviewRequest request) {
        return new Interview {
            Id = request.Id,
            InterviewDate = request.InterviewDate,
            Feedback = request.Feedback,
            Stage = request.Stage,
            Result = request.Result,
        };
    }
}
