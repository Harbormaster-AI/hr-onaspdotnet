
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Certification
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CertificationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Issuer { get; set; } 
 public virtual DateOnly? ValidFrom { get; set; } 
 public virtual DateOnly? ValidTo { get; set; } 
 public virtual string? CredentialId { get; set; } 
public virtual Employee? Employee { get; set; } 
public virtual TrainingCourse? Course { get; set; } 

    public static Certification FromRequest(CertificationRequest request) {
        return new Certification {
            Id = request.Id,
            Name = request.Name,
            Issuer = request.Issuer,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo,
            CredentialId = request.CredentialId,
        };
    }
}
