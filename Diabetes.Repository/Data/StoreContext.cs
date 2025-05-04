using Diabetes.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Diabetes.Repository.Data
{
    public class StoreContext : IdentityDbContext<AppUser>

    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        // DbSets للكيانات الأساسية
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clerk> Clerks { get; set; }
        public DbSet<CasualUser> CasualUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }

        // DbSets للقياسات والتتبع
        public DbSet<BloodSugar> BloodSugars { get; set; }
        public DbSet<Alarm> Alarms { get; set; }

        // DbSets للشات بوت
        public DbSet<ChatbotQuestionCasualUser> ChatbotQuestionsCasualUsers { get; set; }
        public DbSet<ChatbotAnswerCasualUser> ChatbotAnswersCasualUsers { get; set; }
        public DbSet<ChatbotQuestionDoctor> ChatbotQuestionsDoctors { get; set; }
        public DbSet<ChatbotAnswerDoctor> ChatbotAnswersDoctors { get; set; }
        public DbSet<ChatbotResultCasualUser> ChatbotResultsCasualUsers { get; set; }

        // DbSets للعلاقات الطبية
        public DbSet<DoctorPatient> DoctorPatients { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<DiagnosisType> DiagnosisTypes { get; set; }

        // DbSets للموافقات والتنظيم
        public DbSet<DoctorApproval> DoctorApprovals { get; set; }

        // DbSets للتغذية والطعام
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<SuggestedFood> SuggestedFoods { get; set; }

        // DbSets للمحتوى
        public DbSet<Post> Posts { get; set; }

    }
}