
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class JobFamily
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? JobfamilyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<JobProfile> JobProfiles { get; set; } = new List<JobProfile>();

    public static JobFamily FromRequest(JobFamilyRequest request) {
        return new JobFamily {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };
    }
}
