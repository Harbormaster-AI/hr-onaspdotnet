using hronaspdotnet.Domain;

namespace hronaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class OrganizationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? RegistrationCountry { get; set; } 
 public virtual string? Website { get; set; } 
}

public class OrganizationResponse : OrganizationRequest {
    public static OrganizationResponse FromModel(Organization model) {
        return new OrganizationResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            RegistrationCountry = model.RegistrationCountry,
            Website = model.Website,
        };
    }
}

public class DepartmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
}

public class DepartmentResponse : DepartmentRequest {
    public static DepartmentResponse FromModel(Department model) {
        return new DepartmentResponse {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
        };
    }
}

public class LocationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
}

public class LocationResponse : LocationRequest {
    public static LocationResponse FromModel(Location model) {
        return new LocationResponse {
            Id = model.Id,
            Name = model.Name,
            Address = model.Address,
            Timezone = model.Timezone,
        };
    }
}

public class CostCenterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Name { get; set; } 
}

public class CostCenterResponse : CostCenterRequest {
    public static CostCenterResponse FromModel(CostCenter model) {
        return new CostCenterResponse {
            Id = model.Id,
            Code = model.Code,
            Name = model.Name,
        };
    }
}

public class JobFamilyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
}

public class JobFamilyResponse : JobFamilyRequest {
    public static JobFamilyResponse FromModel(JobFamily model) {
        return new JobFamilyResponse {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };
    }
}

public class JobProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? JobCode { get; set; } 
 public virtual JobLevel? JobLevel { get; set; } 
 public virtual ExemptStatus? ExemptStatus { get; set; } 
}

public class JobProfileResponse : JobProfileRequest {
    public static JobProfileResponse FromModel(JobProfile model) {
        return new JobProfileResponse {
            Id = model.Id,
            Title = model.Title,
            JobCode = model.JobCode,
            JobLevel = model.JobLevel,
            ExemptStatus = model.ExemptStatus,
        };
    }
}

public class CompetencyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Category { get; set; } 
}

public class CompetencyResponse : CompetencyRequest {
    public static CompetencyResponse FromModel(Competency model) {
        return new CompetencyResponse {
            Id = model.Id,
            Name = model.Name,
            Category = model.Category,
        };
    }
}

public class PositionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PositionCode { get; set; } 
 public virtual decimal? Fte { get; set; } 
 public virtual PositionStatus? Status { get; set; } 
 public virtual WorkLocationType? WorkLocationType { get; set; } 
}

public class PositionResponse : PositionRequest {
    public static PositionResponse FromModel(Position model) {
        return new PositionResponse {
            Id = model.Id,
            PositionCode = model.PositionCode,
            Fte = model.Fte,
            Status = model.Status,
            WorkLocationType = model.WorkLocationType,
        };
    }
}

public class EmployeeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EmployeeNumber { get; set; } 
 public virtual PersonName? Name { get; set; } 
 public virtual Email? WorkEmail { get; set; } 
 public virtual PhoneNumber? WorkPhone { get; set; } 
 public virtual DateOnly? DateOfHire { get; set; } 
 public virtual NationalID? NationalId { get; set; } 
 public virtual EmploymentStatus? Status { get; set; } 
}

public class EmployeeResponse : EmployeeRequest {
    public static EmployeeResponse FromModel(Employee model) {
        return new EmployeeResponse {
            Id = model.Id,
            EmployeeNumber = model.EmployeeNumber,
            Name = model.Name,
            WorkEmail = model.WorkEmail,
            WorkPhone = model.WorkPhone,
            DateOfHire = model.DateOfHire,
            NationalId = model.NationalId,
            Status = model.Status,
        };
    }
}

public class EmploymentAssignmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual bool? Primary { get; set; } 
 public virtual AssignmentType? AssignmentType { get; set; } 
 public virtual AssignmentStatus? Status { get; set; } 
}

public class EmploymentAssignmentResponse : EmploymentAssignmentRequest {
    public static EmploymentAssignmentResponse FromModel(EmploymentAssignment model) {
        return new EmploymentAssignmentResponse {
            Id = model.Id,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Primary = model.Primary,
            AssignmentType = model.AssignmentType,
            Status = model.Status,
        };
    }
}

public class EmploymentContractRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ContractNumber { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual decimal? WorkHoursPerWeek { get; set; } 
 public virtual EmploymentType? EmploymentType { get; set; } 
 public virtual ContractStatus? Status { get; set; } 
 public virtual PayFrequency? PayFrequency { get; set; } 
}

public class EmploymentContractResponse : EmploymentContractRequest {
    public static EmploymentContractResponse FromModel(EmploymentContract model) {
        return new EmploymentContractResponse {
            Id = model.Id,
            ContractNumber = model.ContractNumber,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            WorkHoursPerWeek = model.WorkHoursPerWeek,
            EmploymentType = model.EmploymentType,
            Status = model.Status,
            PayFrequency = model.PayFrequency,
        };
    }
}

public class WorkScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual decimal? StandardHoursPerWeek { get; set; } 
 public virtual ScheduleType? ScheduleType { get; set; } 
}

public class WorkScheduleResponse : WorkScheduleRequest {
    public static WorkScheduleResponse FromModel(WorkSchedule model) {
        return new WorkScheduleResponse {
            Id = model.Id,
            Name = model.Name,
            StandardHoursPerWeek = model.StandardHoursPerWeek,
            ScheduleType = model.ScheduleType,
        };
    }
}

public class WorkShiftRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual LocalTime? StartTime { get; set; } 
 public virtual LocalTime? EndTime { get; set; } 
 public virtual int? BreakMinutes { get; set; } 
 public virtual DayOfWeek_? DayOfWeek_ { get; set; } 
}

public class WorkShiftResponse : WorkShiftRequest {
    public static WorkShiftResponse FromModel(WorkShift model) {
        return new WorkShiftResponse {
            Id = model.Id,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            BreakMinutes = model.BreakMinutes,
            DayOfWeek_ = model.DayOfWeek_,
        };
    }
}

public class ScheduleExceptionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? Date { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual decimal? Hours { get; set; } 
}

public class ScheduleExceptionResponse : ScheduleExceptionRequest {
    public static ScheduleExceptionResponse FromModel(ScheduleException model) {
        return new ScheduleExceptionResponse {
            Id = model.Id,
            Date = model.Date,
            Reason = model.Reason,
            Hours = model.Hours,
        };
    }
}

public class CompensationPackageRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
 public virtual string? Currency { get; set; } 
}

public class CompensationPackageResponse : CompensationPackageRequest {
    public static CompensationPackageResponse FromModel(CompensationPackage model) {
        return new CompensationPackageResponse {
            Id = model.Id,
            EffectiveFrom = model.EffectiveFrom,
            EffectiveTo = model.EffectiveTo,
            Currency = model.Currency,
        };
    }
}

public class SalaryComponentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Amount { get; set; } 
 public virtual bool? Recurring { get; set; } 
 public virtual SalaryComponentType? ComponentType { get; set; } 
}

public class SalaryComponentResponse : SalaryComponentRequest {
    public static SalaryComponentResponse FromModel(SalaryComponent model) {
        return new SalaryComponentResponse {
            Id = model.Id,
            Amount = model.Amount,
            Recurring = model.Recurring,
            ComponentType = model.ComponentType,
        };
    }
}

public class BonusPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Percentage? TargetPercentage { get; set; } 
}

public class BonusPlanResponse : BonusPlanRequest {
    public static BonusPlanResponse FromModel(BonusPlan model) {
        return new BonusPlanResponse {
            Id = model.Id,
            Name = model.Name,
            TargetPercentage = model.TargetPercentage,
        };
    }
}

public class EquityGrantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? GrantId { get; set; } 
 public virtual int? GrantedUnits { get; set; } 
 public virtual DateOnly? VestingStart { get; set; } 
 public virtual EquityType? GrantType { get; set; } 
}

public class EquityGrantResponse : EquityGrantRequest {
    public static EquityGrantResponse FromModel(EquityGrant model) {
        return new EquityGrantResponse {
            Id = model.Id,
            GrantId = model.GrantId,
            GrantedUnits = model.GrantedUnits,
            VestingStart = model.VestingStart,
            GrantType = model.GrantType,
        };
    }
}

public class BenefitPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ProviderName { get; set; } 
 public virtual Percentage? EmployeeContributionRate { get; set; } 
 public virtual Percentage? EmployerContributionRate { get; set; } 
 public virtual string? EligibilityRules { get; set; } 
 public virtual BenefitType? BenefitType { get; set; } 
}

public class BenefitPlanResponse : BenefitPlanRequest {
    public static BenefitPlanResponse FromModel(BenefitPlan model) {
        return new BenefitPlanResponse {
            Id = model.Id,
            Name = model.Name,
            ProviderName = model.ProviderName,
            EmployeeContributionRate = model.EmployeeContributionRate,
            EmployerContributionRate = model.EmployerContributionRate,
            EligibilityRules = model.EligibilityRules,
            BenefitType = model.BenefitType,
        };
    }
}

public class BenefitEnrollmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EnrollmentId { get; set; } 
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
 public virtual BenefitEnrollmentStatus? Status { get; set; } 
 public virtual CoverageLevel? CoverageLevel { get; set; } 
}

public class BenefitEnrollmentResponse : BenefitEnrollmentRequest {
    public static BenefitEnrollmentResponse FromModel(BenefitEnrollment model) {
        return new BenefitEnrollmentResponse {
            Id = model.Id,
            EnrollmentId = model.EnrollmentId,
            EffectiveFrom = model.EffectiveFrom,
            EffectiveTo = model.EffectiveTo,
            Status = model.Status,
            CoverageLevel = model.CoverageLevel,
        };
    }
}

public class DependentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual DateOnly? BirthDate { get; set; } 
 public virtual DependentRelationship? Relationship { get; set; } 
}

public class DependentResponse : DependentRequest {
    public static DependentResponse FromModel(Dependent model) {
        return new DependentResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Relationship = model.Relationship,
        };
    }
}

public class PayrollCalendarRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual PayFrequency? PayFrequency { get; set; } 
}

public class PayrollCalendarResponse : PayrollCalendarRequest {
    public static PayrollCalendarResponse FromModel(PayrollCalendar model) {
        return new PayrollCalendarResponse {
            Id = model.Id,
            Name = model.Name,
            Country = model.Country,
            PayFrequency = model.PayFrequency,
        };
    }
}

public class PayrollRunRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RunNumber { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
 public virtual PayrollStatus? Status { get; set; } 
}

public class PayrollRunResponse : PayrollRunRequest {
    public static PayrollRunResponse FromModel(PayrollRun model) {
        return new PayrollRunResponse {
            Id = model.Id,
            RunNumber = model.RunNumber,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            PaymentDate = model.PaymentDate,
            Status = model.Status,
        };
    }
}

public class PayrollItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Amount { get; set; } 
 public virtual bool? Taxable { get; set; } 
 public virtual PayrollItemType? ItemType { get; set; } 
}

public class PayrollItemResponse : PayrollItemRequest {
    public static PayrollItemResponse FromModel(PayrollItem model) {
        return new PayrollItemResponse {
            Id = model.Id,
            Amount = model.Amount,
            Taxable = model.Taxable,
            ItemType = model.ItemType,
        };
    }
}

public class TaxWithholdingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual TaxId? TaxId { get; set; } 
 public virtual int? Allowances { get; set; } 
 public virtual Money? AdditionalAmount { get; set; } 
 public virtual FilingStatus? FilingStatus { get; set; } 
}

public class TaxWithholdingResponse : TaxWithholdingRequest {
    public static TaxWithholdingResponse FromModel(TaxWithholding model) {
        return new TaxWithholdingResponse {
            Id = model.Id,
            TaxId = model.TaxId,
            Allowances = model.Allowances,
            AdditionalAmount = model.AdditionalAmount,
            FilingStatus = model.FilingStatus,
        };
    }
}

public class PaymentMethodRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual bool? Preferred { get; set; } 
 public virtual PaymentMethodType? MethodType { get; set; } 
}

public class PaymentMethodResponse : PaymentMethodRequest {
    public static PaymentMethodResponse FromModel(PaymentMethod model) {
        return new PaymentMethodResponse {
            Id = model.Id,
            Preferred = model.Preferred,
            MethodType = model.MethodType,
        };
    }
}

public class TimesheetRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual DateOnly? SubmissionDate { get; set; } 
 public virtual TimesheetStatus? Status { get; set; } 
}

public class TimesheetResponse : TimesheetRequest {
    public static TimesheetResponse FromModel(Timesheet model) {
        return new TimesheetResponse {
            Id = model.Id,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            SubmissionDate = model.SubmissionDate,
            Status = model.Status,
        };
    }
}

public class TimeEntryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? EntryDate { get; set; } 
 public virtual decimal? HoursWorked { get; set; } 
 public virtual TimeEntryType? EntryType { get; set; } 
}

public class TimeEntryResponse : TimeEntryRequest {
    public static TimeEntryResponse FromModel(TimeEntry model) {
        return new TimeEntryResponse {
            Id = model.Id,
            EntryDate = model.EntryDate,
            HoursWorked = model.HoursWorked,
            EntryType = model.EntryType,
        };
    }
}

public class ApprovalRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ApproverComment { get; set; } 
 public virtual DateOnly? ActionDate { get; set; } 
 public virtual ApprovalStatus? Status { get; set; } 
}

public class ApprovalResponse : ApprovalRequest {
    public static ApprovalResponse FromModel(Approval model) {
        return new ApprovalResponse {
            Id = model.Id,
            ApproverComment = model.ApproverComment,
            ActionDate = model.ActionDate,
            Status = model.Status,
        };
    }
}

public class LeavePolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual decimal? AccrualRate { get; set; } 
 public virtual bool? CarryoverAllowed { get; set; } 
 public virtual decimal? MaxBalance { get; set; } 
 public virtual LeaveCategory? LeaveCategory { get; set; } 
 public virtual AccrualUnit? AccrualUnit { get; set; } 
}

public class LeavePolicyResponse : LeavePolicyRequest {
    public static LeavePolicyResponse FromModel(LeavePolicy model) {
        return new LeavePolicyResponse {
            Id = model.Id,
            Name = model.Name,
            AccrualRate = model.AccrualRate,
            CarryoverAllowed = model.CarryoverAllowed,
            MaxBalance = model.MaxBalance,
            LeaveCategory = model.LeaveCategory,
            AccrualUnit = model.AccrualUnit,
        };
    }
}

public class LeaveRequestRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RequestNumber { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual decimal? Hours { get; set; } 
 public virtual LeaveStatus? Status { get; set; } 
}

public class LeaveRequestResponse : LeaveRequestRequest {
    public static LeaveRequestResponse FromModel(LeaveRequest model) {
        return new LeaveRequestResponse {
            Id = model.Id,
            RequestNumber = model.RequestNumber,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Reason = model.Reason,
            Hours = model.Hours,
            Status = model.Status,
        };
    }
}

public class PerformanceCycleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual CycleStatus? Status { get; set; } 
}

public class PerformanceCycleResponse : PerformanceCycleRequest {
    public static PerformanceCycleResponse FromModel(PerformanceCycle model) {
        return new PerformanceCycleResponse {
            Id = model.Id,
            Name = model.Name,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = model.Status,
        };
    }
}

public class GoalRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? TargetDate { get; set; } 
 public virtual Percentage? Weight { get; set; } 
 public virtual GoalStatus? Status { get; set; } 
}

public class GoalResponse : GoalRequest {
    public static GoalResponse FromModel(Goal model) {
        return new GoalResponse {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            TargetDate = model.TargetDate,
            Weight = model.Weight,
            Status = model.Status,
        };
    }
}

public class PerformanceReviewRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReviewNumber { get; set; } 
 public virtual DateOnly? ReviewDate { get; set; } 
 public virtual string? ReviewerComments { get; set; } 
 public virtual PerformanceRating? Rating { get; set; } 
 public virtual ReviewStatus? Status { get; set; } 
}

public class PerformanceReviewResponse : PerformanceReviewRequest {
    public static PerformanceReviewResponse FromModel(PerformanceReview model) {
        return new PerformanceReviewResponse {
            Id = model.Id,
            ReviewNumber = model.ReviewNumber,
            ReviewDate = model.ReviewDate,
            ReviewerComments = model.ReviewerComments,
            Rating = model.Rating,
            Status = model.Status,
        };
    }
}

public class CompetencyRatingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Comment { get; set; } 
 public virtual PerformanceRating? Rating { get; set; } 
}

public class CompetencyRatingResponse : CompetencyRatingRequest {
    public static CompetencyRatingResponse FromModel(CompetencyRating model) {
        return new CompetencyRatingResponse {
            Id = model.Id,
            Comment = model.Comment,
            Rating = model.Rating,
        };
    }
}

public class TrainingCourseRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual decimal? DurationHours { get; set; } 
 public virtual DeliveryMethod? DeliveryMethod { get; set; } 
}

public class TrainingCourseResponse : TrainingCourseRequest {
    public static TrainingCourseResponse FromModel(TrainingCourse model) {
        return new TrainingCourseResponse {
            Id = model.Id,
            Code = model.Code,
            Title = model.Title,
            DurationHours = model.DurationHours,
            DeliveryMethod = model.DeliveryMethod,
        };
    }
}

public class TrainingEnrollmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EnrollmentNumber { get; set; } 
 public virtual DateOnly? CompletionDate { get; set; } 
 public virtual decimal? Score { get; set; } 
 public virtual TrainingStatus? Status { get; set; } 
}

public class TrainingEnrollmentResponse : TrainingEnrollmentRequest {
    public static TrainingEnrollmentResponse FromModel(TrainingEnrollment model) {
        return new TrainingEnrollmentResponse {
            Id = model.Id,
            EnrollmentNumber = model.EnrollmentNumber,
            CompletionDate = model.CompletionDate,
            Score = model.Score,
            Status = model.Status,
        };
    }
}

public class CertificationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Issuer { get; set; } 
 public virtual DateOnly? ValidFrom { get; set; } 
 public virtual DateOnly? ValidTo { get; set; } 
 public virtual string? CredentialId { get; set; } 
}

public class CertificationResponse : CertificationRequest {
    public static CertificationResponse FromModel(Certification model) {
        return new CertificationResponse {
            Id = model.Id,
            Name = model.Name,
            Issuer = model.Issuer,
            ValidFrom = model.ValidFrom,
            ValidTo = model.ValidTo,
            CredentialId = model.CredentialId,
        };
    }
}

public class JobRequisitionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RequisitionNumber { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual int? Openings { get; set; } 
 public virtual DateOnly? TargetStartDate { get; set; } 
 public virtual RequisitionStatus? Status { get; set; } 
 public virtual RequisitionPriority? Priority { get; set; } 
}

public class JobRequisitionResponse : JobRequisitionRequest {
    public static JobRequisitionResponse FromModel(JobRequisition model) {
        return new JobRequisitionResponse {
            Id = model.Id,
            RequisitionNumber = model.RequisitionNumber,
            Title = model.Title,
            Openings = model.Openings,
            TargetStartDate = model.TargetStartDate,
            Status = model.Status,
            Priority = model.Priority,
        };
    }
}

public class CandidateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual PersonName? Name { get; set; } 
 public virtual Email? Email { get; set; } 
 public virtual PhoneNumber? Phone { get; set; } 
 public virtual CandidateSource? Source { get; set; } 
}

public class CandidateResponse : CandidateRequest {
    public static CandidateResponse FromModel(Candidate model) {
        return new CandidateResponse {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Source = model.Source,
        };
    }
}

public class JobApplicationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ApplicationNumber { get; set; } 
 public virtual DateOnly? AppliedDate { get; set; } 
 public virtual string? ResumeUrl { get; set; } 
 public virtual ApplicationStatus? Status { get; set; } 
}

public class JobApplicationResponse : JobApplicationRequest {
    public static JobApplicationResponse FromModel(JobApplication model) {
        return new JobApplicationResponse {
            Id = model.Id,
            ApplicationNumber = model.ApplicationNumber,
            AppliedDate = model.AppliedDate,
            ResumeUrl = model.ResumeUrl,
            Status = model.Status,
        };
    }
}

public class InterviewRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? InterviewDate { get; set; } 
 public virtual string? Feedback { get; set; } 
 public virtual InterviewStage? Stage { get; set; } 
 public virtual InterviewResult? Result { get; set; } 
}

public class InterviewResponse : InterviewRequest {
    public static InterviewResponse FromModel(Interview model) {
        return new InterviewResponse {
            Id = model.Id,
            InterviewDate = model.InterviewDate,
            Feedback = model.Feedback,
            Stage = model.Stage,
            Result = model.Result,
        };
    }
}

public class ScreeningRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DateOnly? CompletedDate { get; set; } 
 public virtual BackgroundCheckStatus? Status { get; set; } 
}

public class ScreeningResponse : ScreeningRequest {
    public static ScreeningResponse FromModel(Screening model) {
        return new ScreeningResponse {
            Id = model.Id,
            Name = model.Name,
            CompletedDate = model.CompletedDate,
            Status = model.Status,
        };
    }
}

public class OfferRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OfferNumber { get; set; } 
 public virtual DateOnly? ProposedStartDate { get; set; } 
 public virtual Money? BaseSalary { get; set; } 
 public virtual Money? SignOnBonus { get; set; } 
 public virtual OfferStatus? Status { get; set; } 
}

public class OfferResponse : OfferRequest {
    public static OfferResponse FromModel(Offer model) {
        return new OfferResponse {
            Id = model.Id,
            OfferNumber = model.OfferNumber,
            ProposedStartDate = model.ProposedStartDate,
            BaseSalary = model.BaseSalary,
            SignOnBonus = model.SignOnBonus,
            Status = model.Status,
        };
    }
}

public class OnboardingTaskRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TaskNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual OnboardingTaskStatus? Status { get; set; } 
}

public class OnboardingTaskResponse : OnboardingTaskRequest {
    public static OnboardingTaskResponse FromModel(OnboardingTask model) {
        return new OnboardingTaskResponse {
            Id = model.Id,
            TaskNumber = model.TaskNumber,
            Name = model.Name,
            DueDate = model.DueDate,
            Status = model.Status,
        };
    }
}

public class BackgroundCheckRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CheckNumber { get; set; } 
 public virtual string? Provider { get; set; } 
 public virtual DateOnly? CompletedDate { get; set; } 
 public virtual BackgroundCheckStatus? Status { get; set; } 
}

public class BackgroundCheckResponse : BackgroundCheckRequest {
    public static BackgroundCheckResponse FromModel(BackgroundCheck model) {
        return new BackgroundCheckResponse {
            Id = model.Id,
            CheckNumber = model.CheckNumber,
            Provider = model.Provider,
            CompletedDate = model.CompletedDate,
            Status = model.Status,
        };
    }
}

public class DocumentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? FileUrl { get; set; } 
 public virtual DateOnly? UploadedDate { get; set; } 
 public virtual DocumentType? DocumentType { get; set; } 
}

public class DocumentResponse : DocumentRequest {
    public static DocumentResponse FromModel(Document model) {
        return new DocumentResponse {
            Id = model.Id,
            Name = model.Name,
            FileUrl = model.FileUrl,
            UploadedDate = model.UploadedDate,
            DocumentType = model.DocumentType,
        };
    }
}

public class PolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PolicyNumber { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Description { get; set; } 
}

public class PolicyResponse : PolicyRequest {
    public static PolicyResponse FromModel(Policy model) {
        return new PolicyResponse {
            Id = model.Id,
            PolicyNumber = model.PolicyNumber,
            Name = model.Name,
            EffectiveDate = model.EffectiveDate,
            Description = model.Description,
        };
    }
}

public class PolicyAcknowledgementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AcknowledgementDate { get; set; } 
 public virtual AcknowledgementStatus? Status { get; set; } 
}

public class PolicyAcknowledgementResponse : PolicyAcknowledgementRequest {
    public static PolicyAcknowledgementResponse FromModel(PolicyAcknowledgement model) {
        return new PolicyAcknowledgementResponse {
            Id = model.Id,
            AcknowledgementDate = model.AcknowledgementDate,
            Status = model.Status,
        };
    }
}

public class TerminationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TerminationNumber { get; set; } 
 public virtual DateOnly? TerminationDate { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual bool? EligibleForRehire { get; set; } 
 public virtual TerminationReason? Reason { get; set; } 
 public virtual TerminationType? Type { get; set; } 
}

public class TerminationResponse : TerminationRequest {
    public static TerminationResponse FromModel(Termination model) {
        return new TerminationResponse {
            Id = model.Id,
            TerminationNumber = model.TerminationNumber,
            TerminationDate = model.TerminationDate,
            Notes = model.Notes,
            EligibleForRehire = model.EligibleForRehire,
            Reason = model.Reason,
            Type = model.Type,
        };
    }
}

public class WorkAuthorizationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Country { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
 public virtual WorkAuthorizationStatus? Status { get; set; } 
}

public class WorkAuthorizationResponse : WorkAuthorizationRequest {
    public static WorkAuthorizationResponse FromModel(WorkAuthorization model) {
        return new WorkAuthorizationResponse {
            Id = model.Id,
            Country = model.Country,
            ExpirationDate = model.ExpirationDate,
            Status = model.Status,
        };
    }
}

public class BankAccountRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AccountHolder { get; set; } 
 public virtual string? BankName { get; set; } 
 public virtual string? Iban { get; set; } 
 public virtual string? Bic { get; set; } 
 public virtual string? AccountNumber { get; set; } 
 public virtual string? RoutingNumber { get; set; } 
}

public class BankAccountResponse : BankAccountRequest {
    public static BankAccountResponse FromModel(BankAccount model) {
        return new BankAccountResponse {
            Id = model.Id,
            AccountHolder = model.AccountHolder,
            BankName = model.BankName,
            Iban = model.Iban,
            Bic = model.Bic,
            AccountNumber = model.AccountNumber,
            RoutingNumber = model.RoutingNumber,
        };
    }
}

