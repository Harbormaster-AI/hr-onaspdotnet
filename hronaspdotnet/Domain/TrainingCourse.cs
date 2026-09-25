
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class TrainingCourse
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TrainingcourseId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual decimal? DurationHours { get; set; } 
public virtual ICollection<TrainingCourse> Prerequisites { get; set; } = new List<TrainingCourse>();
public virtual ICollection<TrainingEnrollment> Enrollments { get; set; } = new List<TrainingEnrollment>();
public virtual ICollection<JobProfile> JobProfiles { get; set; } = new List<JobProfile>();
 public virtual DeliveryMethod? DeliveryMethod { get; set; } 

    public static TrainingCourse FromRequest(TrainingCourseRequest request) {
        return new TrainingCourse {
            Id = request.Id,
            Code = request.Code,
            Title = request.Title,
            DurationHours = request.DurationHours,
            DeliveryMethod = request.DeliveryMethod,
        };
    }
}
