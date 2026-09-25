
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class TrainingEnrollment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TrainingenrollmentId { get; set; } 
 public virtual string? EnrollmentNumber { get; set; } 
 public virtual DateOnly? CompletionDate { get; set; } 
 public virtual decimal? Score { get; set; } 
public virtual TrainingCourse? Course { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual Employee? Instructor { get; set; } 
 public virtual TrainingStatus? Status { get; set; } 

    public static TrainingEnrollment FromRequest(TrainingEnrollmentRequest request) {
        return new TrainingEnrollment {
            Id = request.Id,
            EnrollmentNumber = request.EnrollmentNumber,
            CompletionDate = request.CompletionDate,
            Score = request.Score,
            Status = request.Status,
        };
    }
}
