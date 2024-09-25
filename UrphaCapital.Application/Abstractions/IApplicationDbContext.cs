using Microsoft.EntityFrameworkCore;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homeworks> Homeworks { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<ClickTransaction> ClickTransactions { get; set; }

        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
    }
}
