using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        DbSet<User> Users { get; }
        DbSet<UserCourse> UserCourses { get; set; }
        DbSet<GlobalID> GlobalIds { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Mentor> Mentors { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Homeworks> Homeworks { get; set; }
        DbSet<Result> Results { get; set; }
        DbSet<Help> Helps { get; set; }
        DbSet<ClickTransaction> ClickTransactions { get; set; }
        DbSet<Ideas> Ideass { get; set; }
        DbSet<Announcement> Announcements { get; set; }

        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
    }
}
