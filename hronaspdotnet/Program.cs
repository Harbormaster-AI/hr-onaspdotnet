using hronaspdotnet.Api;
using hronaspdotnet.Persistence;
using hronaspdotnet.Service;
using hronaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("hronaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<ILocationRepository, LocationRepository>();
    builder.Services.AddScoped<ICostCenterRepository, CostCenterRepository>();
    builder.Services.AddScoped<IJobFamilyRepository, JobFamilyRepository>();
    builder.Services.AddScoped<IJobProfileRepository, JobProfileRepository>();
    builder.Services.AddScoped<ICompetencyRepository, CompetencyRepository>();
    builder.Services.AddScoped<IPositionRepository, PositionRepository>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IEmploymentAssignmentRepository, EmploymentAssignmentRepository>();
    builder.Services.AddScoped<IEmploymentContractRepository, EmploymentContractRepository>();
    builder.Services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
    builder.Services.AddScoped<IWorkShiftRepository, WorkShiftRepository>();
    builder.Services.AddScoped<IScheduleExceptionRepository, ScheduleExceptionRepository>();
    builder.Services.AddScoped<ICompensationPackageRepository, CompensationPackageRepository>();
    builder.Services.AddScoped<ISalaryComponentRepository, SalaryComponentRepository>();
    builder.Services.AddScoped<IBonusPlanRepository, BonusPlanRepository>();
    builder.Services.AddScoped<IEquityGrantRepository, EquityGrantRepository>();
    builder.Services.AddScoped<IBenefitPlanRepository, BenefitPlanRepository>();
    builder.Services.AddScoped<IBenefitEnrollmentRepository, BenefitEnrollmentRepository>();
    builder.Services.AddScoped<IDependentRepository, DependentRepository>();
    builder.Services.AddScoped<IPayrollCalendarRepository, PayrollCalendarRepository>();
    builder.Services.AddScoped<IPayrollRunRepository, PayrollRunRepository>();
    builder.Services.AddScoped<IPayrollItemRepository, PayrollItemRepository>();
    builder.Services.AddScoped<ITaxWithholdingRepository, TaxWithholdingRepository>();
    builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
    builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
    builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
    builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
    builder.Services.AddScoped<ILeavePolicyRepository, LeavePolicyRepository>();
    builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
    builder.Services.AddScoped<IPerformanceCycleRepository, PerformanceCycleRepository>();
    builder.Services.AddScoped<IGoalRepository, GoalRepository>();
    builder.Services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
    builder.Services.AddScoped<ICompetencyRatingRepository, CompetencyRatingRepository>();
    builder.Services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
    builder.Services.AddScoped<ITrainingEnrollmentRepository, TrainingEnrollmentRepository>();
    builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
    builder.Services.AddScoped<IJobRequisitionRepository, JobRequisitionRepository>();
    builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
    builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
    builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
    builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
    builder.Services.AddScoped<IOfferRepository, OfferRepository>();
    builder.Services.AddScoped<IOnboardingTaskRepository, OnboardingTaskRepository>();
    builder.Services.AddScoped<IBackgroundCheckRepository, BackgroundCheckRepository>();
    builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
    builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
    builder.Services.AddScoped<IPolicyAcknowledgementRepository, PolicyAcknowledgementRepository>();
    builder.Services.AddScoped<ITerminationRepository, TerminationRepository>();
    builder.Services.AddScoped<IWorkAuthorizationRepository, WorkAuthorizationRepository>();
    builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

    builder.Services.AddScoped<IOrganizationService, OrganizationService>();
    builder.Services.AddScoped<IDepartmentService, DepartmentService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddScoped<ICostCenterService, CostCenterService>();
    builder.Services.AddScoped<IJobFamilyService, JobFamilyService>();
    builder.Services.AddScoped<IJobProfileService, JobProfileService>();
    builder.Services.AddScoped<ICompetencyService, CompetencyService>();
    builder.Services.AddScoped<IPositionService, PositionService>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IEmploymentAssignmentService, EmploymentAssignmentService>();
    builder.Services.AddScoped<IEmploymentContractService, EmploymentContractService>();
    builder.Services.AddScoped<IWorkScheduleService, WorkScheduleService>();
    builder.Services.AddScoped<IWorkShiftService, WorkShiftService>();
    builder.Services.AddScoped<IScheduleExceptionService, ScheduleExceptionService>();
    builder.Services.AddScoped<ICompensationPackageService, CompensationPackageService>();
    builder.Services.AddScoped<ISalaryComponentService, SalaryComponentService>();
    builder.Services.AddScoped<IBonusPlanService, BonusPlanService>();
    builder.Services.AddScoped<IEquityGrantService, EquityGrantService>();
    builder.Services.AddScoped<IBenefitPlanService, BenefitPlanService>();
    builder.Services.AddScoped<IBenefitEnrollmentService, BenefitEnrollmentService>();
    builder.Services.AddScoped<IDependentService, DependentService>();
    builder.Services.AddScoped<IPayrollCalendarService, PayrollCalendarService>();
    builder.Services.AddScoped<IPayrollRunService, PayrollRunService>();
    builder.Services.AddScoped<IPayrollItemService, PayrollItemService>();
    builder.Services.AddScoped<ITaxWithholdingService, TaxWithholdingService>();
    builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
    builder.Services.AddScoped<ITimesheetService, TimesheetService>();
    builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
    builder.Services.AddScoped<IApprovalService, ApprovalService>();
    builder.Services.AddScoped<ILeavePolicyService, LeavePolicyService>();
    builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
    builder.Services.AddScoped<IPerformanceCycleService, PerformanceCycleService>();
    builder.Services.AddScoped<IGoalService, GoalService>();
    builder.Services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
    builder.Services.AddScoped<ICompetencyRatingService, CompetencyRatingService>();
    builder.Services.AddScoped<ITrainingCourseService, TrainingCourseService>();
    builder.Services.AddScoped<ITrainingEnrollmentService, TrainingEnrollmentService>();
    builder.Services.AddScoped<ICertificationService, CertificationService>();
    builder.Services.AddScoped<IJobRequisitionService, JobRequisitionService>();
    builder.Services.AddScoped<ICandidateService, CandidateService>();
    builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
    builder.Services.AddScoped<IInterviewService, InterviewService>();
    builder.Services.AddScoped<IScreeningService, ScreeningService>();
    builder.Services.AddScoped<IOfferService, OfferService>();
    builder.Services.AddScoped<IOnboardingTaskService, OnboardingTaskService>();
    builder.Services.AddScoped<IBackgroundCheckService, BackgroundCheckService>();
    builder.Services.AddScoped<IDocumentService, DocumentService>();
    builder.Services.AddScoped<IPolicyService, PolicyService>();
    builder.Services.AddScoped<IPolicyAcknowledgementService, PolicyAcknowledgementService>();
    builder.Services.AddScoped<ITerminationService, TerminationService>();
    builder.Services.AddScoped<IWorkAuthorizationService, WorkAuthorizationService>();
    builder.Services.AddScoped<IBankAccountService, BankAccountService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapOrganizationEndpoints();
    app.MapDepartmentEndpoints();
    app.MapLocationEndpoints();
    app.MapCostCenterEndpoints();
    app.MapJobFamilyEndpoints();
    app.MapJobProfileEndpoints();
    app.MapCompetencyEndpoints();
    app.MapPositionEndpoints();
    app.MapEmployeeEndpoints();
    app.MapEmploymentAssignmentEndpoints();
    app.MapEmploymentContractEndpoints();
    app.MapWorkScheduleEndpoints();
    app.MapWorkShiftEndpoints();
    app.MapScheduleExceptionEndpoints();
    app.MapCompensationPackageEndpoints();
    app.MapSalaryComponentEndpoints();
    app.MapBonusPlanEndpoints();
    app.MapEquityGrantEndpoints();
    app.MapBenefitPlanEndpoints();
    app.MapBenefitEnrollmentEndpoints();
    app.MapDependentEndpoints();
    app.MapPayrollCalendarEndpoints();
    app.MapPayrollRunEndpoints();
    app.MapPayrollItemEndpoints();
    app.MapTaxWithholdingEndpoints();
    app.MapPaymentMethodEndpoints();
    app.MapTimesheetEndpoints();
    app.MapTimeEntryEndpoints();
    app.MapApprovalEndpoints();
    app.MapLeavePolicyEndpoints();
    app.MapLeaveRequestEndpoints();
    app.MapPerformanceCycleEndpoints();
    app.MapGoalEndpoints();
    app.MapPerformanceReviewEndpoints();
    app.MapCompetencyRatingEndpoints();
    app.MapTrainingCourseEndpoints();
    app.MapTrainingEnrollmentEndpoints();
    app.MapCertificationEndpoints();
    app.MapJobRequisitionEndpoints();
    app.MapCandidateEndpoints();
    app.MapJobApplicationEndpoints();
    app.MapInterviewEndpoints();
    app.MapScreeningEndpoints();
    app.MapOfferEndpoints();
    app.MapOnboardingTaskEndpoints();
    app.MapBackgroundCheckEndpoints();
    app.MapDocumentEndpoints();
    app.MapPolicyEndpoints();
    app.MapPolicyAcknowledgementEndpoints();
    app.MapTerminationEndpoints();
    app.MapWorkAuthorizationEndpoints();
    app.MapBankAccountEndpoints();

app.Run();

public partial class Program { }

