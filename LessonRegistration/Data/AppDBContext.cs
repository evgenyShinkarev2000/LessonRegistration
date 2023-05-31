using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LessonRegistration.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<AAA>(e =>
            //{
            //    e.HasMany(e => e.BBBs).WithMany();
            //});
            //base.OnModelCreating(modelBuilder);
        }
    }
}
