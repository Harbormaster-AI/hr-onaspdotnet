
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Policy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PolicyId { get; set; } 
 public virtual string? PolicyNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Description { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<PolicyAcknowledgement> Acknowledgements { get; set; } = new List<PolicyAcknowledgement>();

    public static Policy FromRequest(PolicyRequest request) {
        return new Policy {
            Id = request.Id,
            PolicyNumber = request.PolicyNumber,
            Name = request.Name,
            EffectiveDate = request.EffectiveDate,
            Description = request.Description,
        };
    }
}
