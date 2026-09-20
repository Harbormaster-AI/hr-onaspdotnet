using Microsoft.EntityFrameworkCore;

using hronaspdotnet.Domain;

namespace hronaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Organization> Organizations => Set<Organization>();
public DbSet<Department> Departments => Set<Department>();
public DbSet<Location> Locations => Set<Location>();
public DbSet<CostCenter> CostCenters => Set<CostCenter>();
public DbSet<JobFamily> JobFamilys => Set<JobFamily>();
public DbSet<JobProfile> JobProfiles => Set<JobProfile>();
public DbSet<Competency> Competencys => Set<Competency>();
public DbSet<Position> Positions => Set<Position>();
public DbSet<Employee> Employees => Set<Employee>();
public DbSet<EmploymentAssignment> EmploymentAssignments => Set<EmploymentAssignment>();
public DbSet<EmploymentContract> EmploymentContracts => Set<EmploymentContract>();
public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();
public DbSet<WorkShift> WorkShifts => Set<WorkShift>();
public DbSet<ScheduleException> ScheduleExceptions => Set<ScheduleException>();
public DbSet<CompensationPackage> CompensationPackages => Set<CompensationPackage>();
public DbSet<SalaryComponent> SalaryComponents => Set<SalaryComponent>();
public DbSet<BonusPlan> BonusPlans => Set<BonusPlan>();
public DbSet<EquityGrant> EquityGrants => Set<EquityGrant>();
public DbSet<BenefitPlan> BenefitPlans => Set<BenefitPlan>();
public DbSet<BenefitEnrollment> BenefitEnrollments => Set<BenefitEnrollment>();
public DbSet<Dependent> Dependents => Set<Dependent>();
public DbSet<PayrollCalendar> PayrollCalendars => Set<PayrollCalendar>();
public DbSet<PayrollRun> PayrollRuns => Set<PayrollRun>();
public DbSet<PayrollItem> PayrollItems => Set<PayrollItem>();
public DbSet<TaxWithholding> TaxWithholdings => Set<TaxWithholding>();
public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
public DbSet<Timesheet> Timesheets => Set<Timesheet>();
public DbSet<TimeEntry> TimeEntrys => Set<TimeEntry>();
public DbSet<Approval> Approvals => Set<Approval>();
public DbSet<LeavePolicy> LeavePolicys => Set<LeavePolicy>();
public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
public DbSet<PerformanceCycle> PerformanceCycles => Set<PerformanceCycle>();
public DbSet<Goal> Goals => Set<Goal>();
public DbSet<PerformanceReview> PerformanceReviews => Set<PerformanceReview>();
public DbSet<CompetencyRating> CompetencyRatings => Set<CompetencyRating>();
public DbSet<TrainingCourse> TrainingCourses => Set<TrainingCourse>();
public DbSet<TrainingEnrollment> TrainingEnrollments => Set<TrainingEnrollment>();
public DbSet<Certification> Certifications => Set<Certification>();
public DbSet<JobRequisition> JobRequisitions => Set<JobRequisition>();
public DbSet<Candidate> Candidates => Set<Candidate>();
public DbSet<JobApplication> JobApplications => Set<JobApplication>();
public DbSet<Interview> Interviews => Set<Interview>();
public DbSet<Screening> Screenings => Set<Screening>();
public DbSet<Offer> Offers => Set<Offer>();
public DbSet<OnboardingTask> OnboardingTasks => Set<OnboardingTask>();
public DbSet<BackgroundCheck> BackgroundChecks => Set<BackgroundCheck>();
public DbSet<Document> Documents => Set<Document>();
public DbSet<Policy> Policys => Set<Policy>();
public DbSet<PolicyAcknowledgement> PolicyAcknowledgements => Set<PolicyAcknowledgement>();
public DbSet<Termination> Terminations => Set<Termination>();
public DbSet<WorkAuthorization> WorkAuthorizations => Set<WorkAuthorization>();
public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Organization has one or more Departments of type Department
        modelBuilder.Entity<Department>()
            .HasOne<Organization>()
            .WithMany(parent => parent.Departments)
            .HasForeignKey("DepartmentsId");

        // Organization has one or more Locations of type Location
        modelBuilder.Entity<Location>()
            .HasOne<Organization>()
            .WithMany(parent => parent.Locations)
            .HasForeignKey("LocationsId");

        // Organization has one or more JobFamilies of type JobFamily
        modelBuilder.Entity<JobFamily>()
            .HasOne<Organization>()
            .WithMany(parent => parent.JobFamilies)
            .HasForeignKey("JobFamiliesId");

        // Organization has one or more BenefitPlans of type BenefitPlan
        modelBuilder.Entity<BenefitPlan>()
            .HasOne<Organization>()
            .WithMany(parent => parent.BenefitPlans)
            .HasForeignKey("BenefitPlansId");

        // Organization has one or more CostCenters of type CostCenter
        modelBuilder.Entity<CostCenter>()
            .HasOne<Organization>()
            .WithMany(parent => parent.CostCenters)
            .HasForeignKey("CostCentersId");

        // Organization has one or more PayrollCalendars of type PayrollCalendar
        modelBuilder.Entity<PayrollCalendar>()
            .HasOne<Organization>()
            .WithMany(parent => parent.PayrollCalendars)
            .HasForeignKey("PayrollCalendarsId");

        // Department has one Organization of type Organization
        modelBuilder.Entity<Department>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");

        // Department has one Manager of type Employee
        modelBuilder.Entity<Department>()
            .HasOne(x => x.Manager)
            .WithMany()
            .HasForeignKey("ManagerId");

        // Department has one CostCenter of type CostCenter
        modelBuilder.Entity<Department>()
            .HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey("CostCenterId");


        // Department has one or more Positions of type Position
        modelBuilder.Entity<Position>()
            .HasOne<Department>()
            .WithMany(parent => parent.Positions)
            .HasForeignKey("PositionsId");

        // Department has one or more Employees of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<Department>()
            .WithMany(parent => parent.Employees)
            .HasForeignKey("EmployeesId");

        // Location has one Organization of type Organization
        modelBuilder.Entity<Location>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Location has one or more Departments of type Department
        modelBuilder.Entity<Department>()
            .HasOne<Location>()
            .WithMany(parent => parent.Departments)
            .HasForeignKey("DepartmentsId");

        // Location has one or more Positions of type Position
        modelBuilder.Entity<Position>()
            .HasOne<Location>()
            .WithMany(parent => parent.Positions)
            .HasForeignKey("PositionsId");

        // Location has one or more Employees of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<Location>()
            .WithMany(parent => parent.Employees)
            .HasForeignKey("EmployeesId");

        // CostCenter has one Organization of type Organization
        modelBuilder.Entity<CostCenter>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // CostCenter has one or more Departments of type Department
        modelBuilder.Entity<Department>()
            .HasOne<CostCenter>()
            .WithMany(parent => parent.Departments)
            .HasForeignKey("DepartmentsId");

        // CostCenter has one or more Positions of type Position
        modelBuilder.Entity<Position>()
            .HasOne<CostCenter>()
            .WithMany(parent => parent.Positions)
            .HasForeignKey("PositionsId");

        // CostCenter has one or more Employees of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<CostCenter>()
            .WithMany(parent => parent.Employees)
            .HasForeignKey("EmployeesId");

        // JobFamily has one Organization of type Organization
        modelBuilder.Entity<JobFamily>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // JobFamily has one or more JobProfiles of type JobProfile
        modelBuilder.Entity<JobProfile>()
            .HasOne<JobFamily>()
            .WithMany(parent => parent.JobProfiles)
            .HasForeignKey("JobProfilesId");

        // JobProfile has one JobFamily of type JobFamily
        modelBuilder.Entity<JobProfile>()
            .HasOne(x => x.JobFamily)
            .WithMany()
            .HasForeignKey("JobFamilyId");


        // JobProfile has one or more Competencies of type Competency
        modelBuilder.Entity<Competency>()
            .HasOne<JobProfile>()
            .WithMany(parent => parent.Competencies)
            .HasForeignKey("CompetenciesId");

        // JobProfile has one or more TrainingRecommendations of type TrainingCourse
        modelBuilder.Entity<TrainingCourse>()
            .HasOne<JobProfile>()
            .WithMany(parent => parent.TrainingRecommendations)
            .HasForeignKey("TrainingRecommendationsId");

        // JobProfile has one or more Positions of type Position
        modelBuilder.Entity<Position>()
            .HasOne<JobProfile>()
            .WithMany(parent => parent.Positions)
            .HasForeignKey("PositionsId");


        // Competency has one or more JobProfiles of type JobProfile
        modelBuilder.Entity<JobProfile>()
            .HasOne<Competency>()
            .WithMany(parent => parent.JobProfiles)
            .HasForeignKey("JobProfilesId");

        // Competency has one or more CompetencyRatings of type CompetencyRating
        modelBuilder.Entity<CompetencyRating>()
            .HasOne<Competency>()
            .WithMany(parent => parent.CompetencyRatings)
            .HasForeignKey("CompetencyRatingsId");

        // Position has one Department of type Department
        modelBuilder.Entity<Position>()
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey("DepartmentId");

        // Position has one JobProfile of type JobProfile
        modelBuilder.Entity<Position>()
            .HasOne(x => x.JobProfile)
            .WithMany()
            .HasForeignKey("JobProfileId");

        // Position has one CostCenter of type CostCenter
        modelBuilder.Entity<Position>()
            .HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey("CostCenterId");

        // Position has one Location of type Location
        modelBuilder.Entity<Position>()
            .HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey("LocationId");

        // Position has one ManagerPosition of type Position
        modelBuilder.Entity<Position>()
            .HasOne(x => x.ManagerPosition)
            .WithMany()
            .HasForeignKey("ManagerPositionId");


        // Position has one or more DirectReports of type Position
        modelBuilder.Entity<Position>()
            .HasOne<Position>()
            .WithMany(parent => parent.DirectReports)
            .HasForeignKey("DirectReportsId");

        // Position has one or more Assignments of type EmploymentAssignment
        modelBuilder.Entity<EmploymentAssignment>()
            .HasOne<Position>()
            .WithMany(parent => parent.Assignments)
            .HasForeignKey("AssignmentsId");

        // Employee has one Manager of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Manager)
            .WithMany()
            .HasForeignKey("ManagerId");

        // Employee has one Department of type Department
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey("DepartmentId");

        // Employee has one PrimaryLocation of type Location
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.PrimaryLocation)
            .WithMany()
            .HasForeignKey("PrimaryLocationId");

        // Employee has one CostCenter of type CostCenter
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey("CostCenterId");


        // Employee has one or more DirectReports of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<Employee>()
            .WithMany(parent => parent.DirectReports)
            .HasForeignKey("DirectReportsId");

        // Employee has one or more EmploymentAssignments of type EmploymentAssignment
        modelBuilder.Entity<EmploymentAssignment>()
            .HasOne<Employee>()
            .WithMany(parent => parent.EmploymentAssignments)
            .HasForeignKey("EmploymentAssignmentsId");

        // Employee has one or more Contracts of type EmploymentContract
        modelBuilder.Entity<EmploymentContract>()
            .HasOne<Employee>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("ContractsId");

        // Employee has one or more BenefitEnrollments of type BenefitEnrollment
        modelBuilder.Entity<BenefitEnrollment>()
            .HasOne<Employee>()
            .WithMany(parent => parent.BenefitEnrollments)
            .HasForeignKey("BenefitEnrollmentsId");

        // Employee has one or more Timesheets of type Timesheet
        modelBuilder.Entity<Timesheet>()
            .HasOne<Employee>()
            .WithMany(parent => parent.Timesheets)
            .HasForeignKey("TimesheetsId");

        // Employee has one or more LeaveRequests of type LeaveRequest
        modelBuilder.Entity<LeaveRequest>()
            .HasOne<Employee>()
            .WithMany(parent => parent.LeaveRequests)
            .HasForeignKey("LeaveRequestsId");

        // Employee has one or more PerformanceReviews of type PerformanceReview
        modelBuilder.Entity<PerformanceReview>()
            .HasOne<Employee>()
            .WithMany(parent => parent.PerformanceReviews)
            .HasForeignKey("PerformanceReviewsId");

        // Employee has one or more TrainingEnrollments of type TrainingEnrollment
        modelBuilder.Entity<TrainingEnrollment>()
            .HasOne<Employee>()
            .WithMany(parent => parent.TrainingEnrollments)
            .HasForeignKey("TrainingEnrollmentsId");

        // Employee has one or more WorkAuthorizations of type WorkAuthorization
        modelBuilder.Entity<WorkAuthorization>()
            .HasOne<Employee>()
            .WithMany(parent => parent.WorkAuthorizations)
            .HasForeignKey("WorkAuthorizationsId");

        // EmploymentAssignment has one Employee of type Employee
        modelBuilder.Entity<EmploymentAssignment>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // EmploymentAssignment has one Position of type Position
        modelBuilder.Entity<EmploymentAssignment>()
            .HasOne(x => x.Position)
            .WithMany()
            .HasForeignKey("PositionId");

        // EmploymentAssignment has one Supervisor of type Employee
        modelBuilder.Entity<EmploymentAssignment>()
            .HasOne(x => x.Supervisor)
            .WithMany()
            .HasForeignKey("SupervisorId");


        // EmploymentContract has one Employee of type Employee
        modelBuilder.Entity<EmploymentContract>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // EmploymentContract has one CompensationPackage of type CompensationPackage
        modelBuilder.Entity<EmploymentContract>()
            .HasOne(x => x.CompensationPackage)
            .WithMany()
            .HasForeignKey("CompensationPackageId");

        // EmploymentContract has one WorkSchedule of type WorkSchedule
        modelBuilder.Entity<EmploymentContract>()
            .HasOne(x => x.WorkSchedule)
            .WithMany()
            .HasForeignKey("WorkScheduleId");

        // EmploymentContract has one Location of type Location
        modelBuilder.Entity<EmploymentContract>()
            .HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey("LocationId");

        // EmploymentContract has one PayrollCalendar of type PayrollCalendar
        modelBuilder.Entity<EmploymentContract>()
            .HasOne(x => x.PayrollCalendar)
            .WithMany()
            .HasForeignKey("PayrollCalendarId");



        // WorkSchedule has one or more Contracts of type EmploymentContract
        modelBuilder.Entity<EmploymentContract>()
            .HasOne<WorkSchedule>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("ContractsId");

        // WorkSchedule has one or more Shifts of type WorkShift
        modelBuilder.Entity<WorkShift>()
            .HasOne<WorkSchedule>()
            .WithMany(parent => parent.Shifts)
            .HasForeignKey("ShiftsId");

        // WorkSchedule has one or more Exceptions of type ScheduleException
        modelBuilder.Entity<ScheduleException>()
            .HasOne<WorkSchedule>()
            .WithMany(parent => parent.Exceptions)
            .HasForeignKey("ExceptionsId");

        // WorkShift has one WorkSchedule of type WorkSchedule
        modelBuilder.Entity<WorkShift>()
            .HasOne(x => x.WorkSchedule)
            .WithMany()
            .HasForeignKey("WorkScheduleId");


        // ScheduleException has one WorkSchedule of type WorkSchedule
        modelBuilder.Entity<ScheduleException>()
            .HasOne(x => x.WorkSchedule)
            .WithMany()
            .HasForeignKey("WorkScheduleId");

        // ScheduleException has one Employee of type Employee
        modelBuilder.Entity<ScheduleException>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // CompensationPackage has one Contract of type EmploymentContract
        modelBuilder.Entity<CompensationPackage>()
            .HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey("ContractId");


        // CompensationPackage has one or more SalaryComponents of type SalaryComponent
        modelBuilder.Entity<SalaryComponent>()
            .HasOne<CompensationPackage>()
            .WithMany(parent => parent.SalaryComponents)
            .HasForeignKey("SalaryComponentsId");

        // CompensationPackage has one or more BonusPlans of type BonusPlan
        modelBuilder.Entity<BonusPlan>()
            .HasOne<CompensationPackage>()
            .WithMany(parent => parent.BonusPlans)
            .HasForeignKey("BonusPlansId");

        // CompensationPackage has one or more EquityGrants of type EquityGrant
        modelBuilder.Entity<EquityGrant>()
            .HasOne<CompensationPackage>()
            .WithMany(parent => parent.EquityGrants)
            .HasForeignKey("EquityGrantsId");

        // SalaryComponent has one CompensationPackage of type CompensationPackage
        modelBuilder.Entity<SalaryComponent>()
            .HasOne(x => x.CompensationPackage)
            .WithMany()
            .HasForeignKey("CompensationPackageId");



        // BonusPlan has one or more CompensationPackages of type CompensationPackage
        modelBuilder.Entity<CompensationPackage>()
            .HasOne<BonusPlan>()
            .WithMany(parent => parent.CompensationPackages)
            .HasForeignKey("CompensationPackagesId");

        // EquityGrant has one CompensationPackage of type CompensationPackage
        modelBuilder.Entity<EquityGrant>()
            .HasOne(x => x.CompensationPackage)
            .WithMany()
            .HasForeignKey("CompensationPackageId");


        // BenefitPlan has one Organization of type Organization
        modelBuilder.Entity<BenefitPlan>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // BenefitPlan has one or more Enrollments of type BenefitEnrollment
        modelBuilder.Entity<BenefitEnrollment>()
            .HasOne<BenefitPlan>()
            .WithMany(parent => parent.Enrollments)
            .HasForeignKey("EnrollmentsId");

        // BenefitEnrollment has one BenefitPlan of type BenefitPlan
        modelBuilder.Entity<BenefitEnrollment>()
            .HasOne(x => x.BenefitPlan)
            .WithMany()
            .HasForeignKey("BenefitPlanId");

        // BenefitEnrollment has one Employee of type Employee
        modelBuilder.Entity<BenefitEnrollment>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // BenefitEnrollment has one or more Dependents of type Dependent
        modelBuilder.Entity<Dependent>()
            .HasOne<BenefitEnrollment>()
            .WithMany(parent => parent.Dependents)
            .HasForeignKey("DependentsId");

        // Dependent has one BenefitEnrollment of type BenefitEnrollment
        modelBuilder.Entity<Dependent>()
            .HasOne(x => x.BenefitEnrollment)
            .WithMany()
            .HasForeignKey("BenefitEnrollmentId");

        // Dependent has one Employee of type Employee
        modelBuilder.Entity<Dependent>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // PayrollCalendar has one Organization of type Organization
        modelBuilder.Entity<PayrollCalendar>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // PayrollCalendar has one or more PayrollRuns of type PayrollRun
        modelBuilder.Entity<PayrollRun>()
            .HasOne<PayrollCalendar>()
            .WithMany(parent => parent.PayrollRuns)
            .HasForeignKey("PayrollRunsId");

        // PayrollCalendar has one or more Employees of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<PayrollCalendar>()
            .WithMany(parent => parent.Employees)
            .HasForeignKey("EmployeesId");

        // PayrollRun has one PayrollCalendar of type PayrollCalendar
        modelBuilder.Entity<PayrollRun>()
            .HasOne(x => x.PayrollCalendar)
            .WithMany()
            .HasForeignKey("PayrollCalendarId");


        // PayrollRun has one or more PayrollItems of type PayrollItem
        modelBuilder.Entity<PayrollItem>()
            .HasOne<PayrollRun>()
            .WithMany(parent => parent.PayrollItems)
            .HasForeignKey("PayrollItemsId");

        // PayrollItem has one PayrollRun of type PayrollRun
        modelBuilder.Entity<PayrollItem>()
            .HasOne(x => x.PayrollRun)
            .WithMany()
            .HasForeignKey("PayrollRunId");

        // PayrollItem has one Employee of type Employee
        modelBuilder.Entity<PayrollItem>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // TaxWithholding has one Employee of type Employee
        modelBuilder.Entity<TaxWithholding>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // PaymentMethod has one Employee of type Employee
        modelBuilder.Entity<PaymentMethod>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // PaymentMethod has one BankAccount of type BankAccount
        modelBuilder.Entity<PaymentMethod>()
            .HasOne(x => x.BankAccount)
            .WithMany()
            .HasForeignKey("BankAccountId");


        // Timesheet has one Employee of type Employee
        modelBuilder.Entity<Timesheet>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // Timesheet has one or more TimeEntries of type TimeEntry
        modelBuilder.Entity<TimeEntry>()
            .HasOne<Timesheet>()
            .WithMany(parent => parent.TimeEntries)
            .HasForeignKey("TimeEntriesId");

        // Timesheet has one or more Approvals of type Approval
        modelBuilder.Entity<Approval>()
            .HasOne<Timesheet>()
            .WithMany(parent => parent.Approvals)
            .HasForeignKey("ApprovalsId");

        // TimeEntry has one Timesheet of type Timesheet
        modelBuilder.Entity<TimeEntry>()
            .HasOne(x => x.Timesheet)
            .WithMany()
            .HasForeignKey("TimesheetId");

        // TimeEntry has one Employee of type Employee
        modelBuilder.Entity<TimeEntry>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // TimeEntry has one CostCenter of type CostCenter
        modelBuilder.Entity<TimeEntry>()
            .HasOne(x => x.CostCenter)
            .WithMany()
            .HasForeignKey("CostCenterId");


        // Approval has one Approver of type Employee
        modelBuilder.Entity<Approval>()
            .HasOne(x => x.Approver)
            .WithMany()
            .HasForeignKey("ApproverId");

        // Approval has one Timesheet of type Timesheet
        modelBuilder.Entity<Approval>()
            .HasOne(x => x.Timesheet)
            .WithMany()
            .HasForeignKey("TimesheetId");

        // Approval has one LeaveRequest of type LeaveRequest
        modelBuilder.Entity<Approval>()
            .HasOne(x => x.LeaveRequest)
            .WithMany()
            .HasForeignKey("LeaveRequestId");


        // LeavePolicy has one Organization of type Organization
        modelBuilder.Entity<LeavePolicy>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // LeavePolicy has one or more LeaveRequests of type LeaveRequest
        modelBuilder.Entity<LeaveRequest>()
            .HasOne<LeavePolicy>()
            .WithMany(parent => parent.LeaveRequests)
            .HasForeignKey("LeaveRequestsId");

        // LeaveRequest has one Employee of type Employee
        modelBuilder.Entity<LeaveRequest>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // LeaveRequest has one LeavePolicy of type LeavePolicy
        modelBuilder.Entity<LeaveRequest>()
            .HasOne(x => x.LeavePolicy)
            .WithMany()
            .HasForeignKey("LeavePolicyId");


        // LeaveRequest has one or more Approvals of type Approval
        modelBuilder.Entity<Approval>()
            .HasOne<LeaveRequest>()
            .WithMany(parent => parent.Approvals)
            .HasForeignKey("ApprovalsId");

        // PerformanceCycle has one Organization of type Organization
        modelBuilder.Entity<PerformanceCycle>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // PerformanceCycle has one or more Reviews of type PerformanceReview
        modelBuilder.Entity<PerformanceReview>()
            .HasOne<PerformanceCycle>()
            .WithMany(parent => parent.Reviews)
            .HasForeignKey("ReviewsId");

        // PerformanceCycle has one or more Goals of type Goal
        modelBuilder.Entity<Goal>()
            .HasOne<PerformanceCycle>()
            .WithMany(parent => parent.Goals)
            .HasForeignKey("GoalsId");

        // Goal has one Employee of type Employee
        modelBuilder.Entity<Goal>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // Goal has one Cycle of type PerformanceCycle
        modelBuilder.Entity<Goal>()
            .HasOne(x => x.Cycle)
            .WithMany()
            .HasForeignKey("CycleId");

        // Goal has one ParentGoal of type Goal
        modelBuilder.Entity<Goal>()
            .HasOne(x => x.ParentGoal)
            .WithMany()
            .HasForeignKey("ParentGoalId");


        // Goal has one or more ChildGoals of type Goal
        modelBuilder.Entity<Goal>()
            .HasOne<Goal>()
            .WithMany(parent => parent.ChildGoals)
            .HasForeignKey("ChildGoalsId");

        // PerformanceReview has one Employee of type Employee
        modelBuilder.Entity<PerformanceReview>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // PerformanceReview has one Reviewer of type Employee
        modelBuilder.Entity<PerformanceReview>()
            .HasOne(x => x.Reviewer)
            .WithMany()
            .HasForeignKey("ReviewerId");

        // PerformanceReview has one Cycle of type PerformanceCycle
        modelBuilder.Entity<PerformanceReview>()
            .HasOne(x => x.Cycle)
            .WithMany()
            .HasForeignKey("CycleId");


        // PerformanceReview has one or more CompetencyRatings of type CompetencyRating
        modelBuilder.Entity<CompetencyRating>()
            .HasOne<PerformanceReview>()
            .WithMany(parent => parent.CompetencyRatings)
            .HasForeignKey("CompetencyRatingsId");

        // PerformanceReview has one or more Goals of type Goal
        modelBuilder.Entity<Goal>()
            .HasOne<PerformanceReview>()
            .WithMany(parent => parent.Goals)
            .HasForeignKey("GoalsId");

        // CompetencyRating has one Review of type PerformanceReview
        modelBuilder.Entity<CompetencyRating>()
            .HasOne(x => x.Review)
            .WithMany()
            .HasForeignKey("ReviewId");

        // CompetencyRating has one Competency of type Competency
        modelBuilder.Entity<CompetencyRating>()
            .HasOne(x => x.Competency)
            .WithMany()
            .HasForeignKey("CompetencyId");



        // TrainingCourse has one or more Prerequisites of type TrainingCourse
        modelBuilder.Entity<TrainingCourse>()
            .HasOne<TrainingCourse>()
            .WithMany(parent => parent.Prerequisites)
            .HasForeignKey("PrerequisitesId");

        // TrainingCourse has one or more Enrollments of type TrainingEnrollment
        modelBuilder.Entity<TrainingEnrollment>()
            .HasOne<TrainingCourse>()
            .WithMany(parent => parent.Enrollments)
            .HasForeignKey("EnrollmentsId");

        // TrainingCourse has one or more JobProfiles of type JobProfile
        modelBuilder.Entity<JobProfile>()
            .HasOne<TrainingCourse>()
            .WithMany(parent => parent.JobProfiles)
            .HasForeignKey("JobProfilesId");

        // TrainingEnrollment has one Course of type TrainingCourse
        modelBuilder.Entity<TrainingEnrollment>()
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey("CourseId");

        // TrainingEnrollment has one Employee of type Employee
        modelBuilder.Entity<TrainingEnrollment>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // TrainingEnrollment has one Instructor of type Employee
        modelBuilder.Entity<TrainingEnrollment>()
            .HasOne(x => x.Instructor)
            .WithMany()
            .HasForeignKey("InstructorId");


        // Certification has one Employee of type Employee
        modelBuilder.Entity<Certification>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // Certification has one Course of type TrainingCourse
        modelBuilder.Entity<Certification>()
            .HasOne(x => x.Course)
            .WithMany()
            .HasForeignKey("CourseId");


        // JobRequisition has one Department of type Department
        modelBuilder.Entity<JobRequisition>()
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey("DepartmentId");

        // JobRequisition has one HiringManager of type Employee
        modelBuilder.Entity<JobRequisition>()
            .HasOne(x => x.HiringManager)
            .WithMany()
            .HasForeignKey("HiringManagerId");

        // JobRequisition has one Recruiter of type Employee
        modelBuilder.Entity<JobRequisition>()
            .HasOne(x => x.Recruiter)
            .WithMany()
            .HasForeignKey("RecruiterId");

        // JobRequisition has one JobProfile of type JobProfile
        modelBuilder.Entity<JobRequisition>()
            .HasOne(x => x.JobProfile)
            .WithMany()
            .HasForeignKey("JobProfileId");


        // JobRequisition has one or more Candidates of type Candidate
        modelBuilder.Entity<Candidate>()
            .HasOne<JobRequisition>()
            .WithMany(parent => parent.Candidates)
            .HasForeignKey("CandidatesId");

        // JobRequisition has one or more Interviews of type Interview
        modelBuilder.Entity<Interview>()
            .HasOne<JobRequisition>()
            .WithMany(parent => parent.Interviews)
            .HasForeignKey("InterviewsId");

        // JobRequisition has one or more Offers of type Offer
        modelBuilder.Entity<Offer>()
            .HasOne<JobRequisition>()
            .WithMany(parent => parent.Offers)
            .HasForeignKey("OffersId");


        // Candidate has one or more Applications of type JobApplication
        modelBuilder.Entity<JobApplication>()
            .HasOne<Candidate>()
            .WithMany(parent => parent.Applications)
            .HasForeignKey("ApplicationsId");

        // Candidate has one or more Interviews of type Interview
        modelBuilder.Entity<Interview>()
            .HasOne<Candidate>()
            .WithMany(parent => parent.Interviews)
            .HasForeignKey("InterviewsId");

        // Candidate has one or more Offers of type Offer
        modelBuilder.Entity<Offer>()
            .HasOne<Candidate>()
            .WithMany(parent => parent.Offers)
            .HasForeignKey("OffersId");

        // Candidate has one or more Documents of type Document
        modelBuilder.Entity<Document>()
            .HasOne<Candidate>()
            .WithMany(parent => parent.Documents)
            .HasForeignKey("DocumentsId");

        // JobApplication has one Candidate of type Candidate
        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey("CandidateId");

        // JobApplication has one Requisition of type JobRequisition
        modelBuilder.Entity<JobApplication>()
            .HasOne(x => x.Requisition)
            .WithMany()
            .HasForeignKey("RequisitionId");


        // JobApplication has one or more Screenings of type Screening
        modelBuilder.Entity<Screening>()
            .HasOne<JobApplication>()
            .WithMany(parent => parent.Screenings)
            .HasForeignKey("ScreeningsId");

        // Interview has one Requisition of type JobRequisition
        modelBuilder.Entity<Interview>()
            .HasOne(x => x.Requisition)
            .WithMany()
            .HasForeignKey("RequisitionId");

        // Interview has one Candidate of type Candidate
        modelBuilder.Entity<Interview>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey("CandidateId");


        // Interview has one or more Interviewers of type Employee
        modelBuilder.Entity<Employee>()
            .HasOne<Interview>()
            .WithMany(parent => parent.Interviewers)
            .HasForeignKey("InterviewersId");

        // Screening has one Application of type JobApplication
        modelBuilder.Entity<Screening>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("ApplicationId");


        // Offer has one Requisition of type JobRequisition
        modelBuilder.Entity<Offer>()
            .HasOne(x => x.Requisition)
            .WithMany()
            .HasForeignKey("RequisitionId");

        // Offer has one Candidate of type Candidate
        modelBuilder.Entity<Offer>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey("CandidateId");

        // Offer has one ApprovedBy of type Employee
        modelBuilder.Entity<Offer>()
            .HasOne(x => x.ApprovedBy)
            .WithMany()
            .HasForeignKey("ApprovedById");

        // Offer has one Contract of type EmploymentContract
        modelBuilder.Entity<Offer>()
            .HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey("ContractId");


        // OnboardingTask has one Employee of type Employee
        modelBuilder.Entity<OnboardingTask>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // OnboardingTask has one AssignedTo of type Employee
        modelBuilder.Entity<OnboardingTask>()
            .HasOne(x => x.AssignedTo)
            .WithMany()
            .HasForeignKey("AssignedToId");

        // OnboardingTask has one RelatedOffer of type Offer
        modelBuilder.Entity<OnboardingTask>()
            .HasOne(x => x.RelatedOffer)
            .WithMany()
            .HasForeignKey("RelatedOfferId");


        // OnboardingTask has one or more Dependencies of type OnboardingTask
        modelBuilder.Entity<OnboardingTask>()
            .HasOne<OnboardingTask>()
            .WithMany(parent => parent.Dependencies)
            .HasForeignKey("DependenciesId");

        // BackgroundCheck has one Candidate of type Candidate
        modelBuilder.Entity<BackgroundCheck>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey("CandidateId");

        // BackgroundCheck has one Requisition of type JobRequisition
        modelBuilder.Entity<BackgroundCheck>()
            .HasOne(x => x.Requisition)
            .WithMany()
            .HasForeignKey("RequisitionId");

        // BackgroundCheck has one Report of type Document
        modelBuilder.Entity<BackgroundCheck>()
            .HasOne(x => x.Report)
            .WithMany()
            .HasForeignKey("ReportId");


        // Document has one Candidate of type Candidate
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey("CandidateId");

        // Document has one Employee of type Employee
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // Policy has one Organization of type Organization
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Policy has one or more Acknowledgements of type PolicyAcknowledgement
        modelBuilder.Entity<PolicyAcknowledgement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Acknowledgements)
            .HasForeignKey("AcknowledgementsId");

        // PolicyAcknowledgement has one Policy of type Policy
        modelBuilder.Entity<PolicyAcknowledgement>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // PolicyAcknowledgement has one Employee of type Employee
        modelBuilder.Entity<PolicyAcknowledgement>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // Termination has one Employee of type Employee
        modelBuilder.Entity<Termination>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");

        // Termination has one Assignment of type EmploymentAssignment
        modelBuilder.Entity<Termination>()
            .HasOne(x => x.Assignment)
            .WithMany()
            .HasForeignKey("AssignmentId");


        // WorkAuthorization has one Employee of type Employee
        modelBuilder.Entity<WorkAuthorization>()
            .HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId");


        // WorkAuthorization has one or more Documents of type Document
        modelBuilder.Entity<Document>()
            .HasOne<WorkAuthorization>()
            .WithMany(parent => parent.Documents)
            .HasForeignKey("DocumentsId");


    }
}
