
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Candidate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CandidateId { get; set; } 
 public virtual PersonName? Name { get; set; } 
 public virtual Email? Email { get; set; } 
 public virtual PhoneNumber? Phone { get; set; } 
public virtual ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
 public virtual CandidateSource? Source { get; set; } 

    public static Candidate FromRequest(CandidateRequest request) {
        return new Candidate {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Source = request.Source,
        };
    }
}
