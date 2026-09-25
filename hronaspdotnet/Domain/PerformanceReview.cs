
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class PerformanceReview
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PerformancereviewId { get; set; } 
 public virtual string? ReviewNumber { get; set; } 
 public virtual DateOnly? ReviewDate { get; set; } 
 public virtual string? ReviewerComments { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual Employee? Reviewer { get; set; } 
public virtual PerformanceCycle? Cycle { get; set; } 
public virtual ICollection<CompetencyRating> CompetencyRatings { get; set; } = new List<CompetencyRating>();
public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
 public virtual PerformanceRating? Rating { get; set; } 
 public virtual ReviewStatus? Status { get; set; } 

    public static PerformanceReview FromRequest(PerformanceReviewRequest request) {
        return new PerformanceReview {
            Id = request.Id,
            ReviewNumber = request.ReviewNumber,
            ReviewDate = request.ReviewDate,
            ReviewerComments = request.ReviewerComments,
            Rating = request.Rating,
            Status = request.Status,
        };
    }
}
