
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Competency
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompetencyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Category { get; set; } 
public virtual ICollection<JobProfile> JobProfiles { get; set; } = new List<JobProfile>();
public virtual ICollection<CompetencyRating> CompetencyRatings { get; set; } = new List<CompetencyRating>();

    public static Competency FromRequest(CompetencyRequest request) {
        return new Competency {
            Id = request.Id,
            Name = request.Name,
            Category = request.Category,
        };
    }
}
