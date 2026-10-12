using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;

namespace SchoolManagementSystem.Services.SchoolManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=SchoolManagementSystem;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Guardian>().ToTable("Guardians");
            
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email})
                .IsUnique();

            modelBuilder.Entity<ClassRoom>()
                .HasIndex(r => new { r.Name, r.SchoolId })
                .IsUnique();
        }
    }
}