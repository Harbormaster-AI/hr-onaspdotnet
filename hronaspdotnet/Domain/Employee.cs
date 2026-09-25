
using hronaspdotnet.Contracts;

namespace hronaspdotnet.Domain;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EmployeeId { get; set; } 
 public virtual string? EmployeeNumber { get; set; } 
 public virtual PersonName? Name { get; set; } 
 public virtual Email? WorkEmail { get; set; } 
 public virtual PhoneNumber? WorkPhone { get; set; } 
 public virtual DateOnly? DateOfHire { get; set; } 
 public virtual NationalID? NationalId { get; set; } 
public virtual Employee? Manager { get; set; } 
public virtual ICollection<Employee> DirectReports { get; set; } = new List<Employee>();
public virtual Department? Department { get; set; } 
public virtual Location? PrimaryLocation { get; set; } 
public virtual CostCenter? CostCenter { get; set; } 
public virtual ICollection<EmploymentAssignment> EmploymentAssignments { get; set; } = new List<EmploymentAssignment>();
public virtual ICollection<EmploymentContract> Contracts { get; set; } = new List<EmploymentContract>();
public virtual ICollection<BenefitEnrollment> BenefitEnrollments { get; set; } = new List<BenefitEnrollment>();
public virtual ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
public virtual ICollection<PerformanceReview> PerformanceReviews { get; set; } = new List<PerformanceReview>();
public virtual ICollection<TrainingEnrollment> TrainingEnrollments { get; set; } = new List<TrainingEnrollment>();
public virtual ICollection<WorkAuthorization> WorkAuthorizations { get; set; } = new List<WorkAuthorization>();
 public virtual EmploymentStatus? Status { get; set; } 

    public static Employee FromRequest(EmployeeRequest request) {
        return new Employee {
            Id = request.Id,
            EmployeeNumber = request.EmployeeNumber,
            Name = request.Name,
            WorkEmail = request.WorkEmail,
            WorkPhone = request.WorkPhone,
            DateOfHire = request.DateOfHire,
            NationalId = request.NationalId,
            Status = request.Status,
        };
    }
}
