
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Offer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OfferId { get; set; } 
 public virtual string? OfferNumber { get; set; } 
 public virtual DateOnly? ProposedStartDate { get; set; } 
 public virtual Money? BaseSalary { get; set; } 
 public virtual Money? SignOnBonus { get; set; } 
public virtual JobRequisition? Requisition { get; set; } 
public virtual Candidate? Candidate { get; set; } 
public virtual Employee? ApprovedBy { get; set; } 
public virtual EmploymentContract? Contract { get; set; } 
 public virtual OfferStatus? Status { get; set; } 

    public static Offer FromRequest(OfferRequest request) {
        return new Offer {
            Id = request.Id,
            OfferNumber = request.OfferNumber,
            ProposedStartDate = request.ProposedStartDate,
            BaseSalary = request.BaseSalary,
            SignOnBonus = request.SignOnBonus,
            Status = request.Status,
        };
    }
}
