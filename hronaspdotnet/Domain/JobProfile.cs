
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class JobProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? JobprofileId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? JobCode { get; set; } 
public virtual JobFamily? JobFamily { get; set; } 
public virtual ICollection<Competency> Competencies { get; set; } = new List<Competency>();
public virtual ICollection<TrainingCourse> TrainingRecommendations { get; set; } = new List<TrainingCourse>();
public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
 public virtual JobLevel? JobLevel { get; set; } 
 public virtual ExemptStatus? ExemptStatus { get; set; } 

    public static JobProfile FromRequest(JobProfileRequest request) {
        return new JobProfile {
            Id = request.Id,
            Title = request.Title,
            JobCode = request.JobCode,
            JobLevel = request.JobLevel,
            ExemptStatus = request.ExemptStatus,
        };
    }
}
