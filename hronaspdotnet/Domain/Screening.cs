
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Screening
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ScreeningId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? CompletedDate { get; set; } 
public virtual JobApplication? Application { get; set; } 
 public virtual BackgroundCheckStatus? Status { get; set; } 

    public static Screening FromRequest(ScreeningRequest request) {
        return new Screening {
            Id = request.Id,
            Name = request.Name,
            CompletedDate = request.CompletedDate,
            Status = request.Status,
        };
    }
}
