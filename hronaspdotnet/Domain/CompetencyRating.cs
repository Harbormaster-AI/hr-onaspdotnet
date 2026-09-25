
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class CompetencyRating
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompetencyratingId { get; set; } 
 public virtual string? Comment { get; set; } 
public virtual PerformanceReview? Review { get; set; } 
public virtual Competency? Competency { get; set; } 
 public virtual PerformanceRating? Rating { get; set; } 

    public static CompetencyRating FromRequest(CompetencyRatingRequest request) {
        return new CompetencyRating {
            Id = request.Id,
            Comment = request.Comment,
            Rating = request.Rating,
        };
    }
}
