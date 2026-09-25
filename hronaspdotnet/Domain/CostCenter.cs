
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class CostCenter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CostcenterId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public static CostCenter FromRequest(CostCenterRequest request) {
        return new CostCenter {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name,
        };
    }
}
