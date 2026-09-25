
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DepartmentId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual Employee? Manager { get; set; } 
public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
public virtual CostCenter? CostCenter { get; set; } 

    public static Department FromRequest(DepartmentRequest request) {
        return new Department {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
        };
    }
}
