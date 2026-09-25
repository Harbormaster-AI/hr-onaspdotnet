
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LocationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public static Location FromRequest(LocationRequest request) {
        return new Location {
            Id = request.Id,
            Name = request.Name,
            Address = request.Address,
            Timezone = request.Timezone,
        };
    }
}
