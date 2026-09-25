
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Dependent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DependentId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual DateOnly? BirthDate { get; set; } 
public virtual BenefitEnrollment? BenefitEnrollment { get; set; } 
public virtual Employee? Employee { get; set; } 
 public virtual DependentRelationship? Relationship { get; set; } 

    public static Dependent FromRequest(DependentRequest request) {
        return new Dependent {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Relationship = request.Relationship,
        };
    }
}
