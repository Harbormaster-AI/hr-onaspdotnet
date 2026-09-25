
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Document
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DocumentId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? FileUrl { get; set; } 
 public virtual DateOnly? UploadedDate { get; set; } 
public virtual Candidate? Candidate { get; set; } 
public virtual Employee? Employee { get; set; } 
 public virtual DocumentType? DocumentType { get; set; } 

    public static Document FromRequest(DocumentRequest request) {
        return new Document {
            Id = request.Id,
            Name = request.Name,
            FileUrl = request.FileUrl,
            UploadedDate = request.UploadedDate,
            DocumentType = request.DocumentType,
        };
    }
}
