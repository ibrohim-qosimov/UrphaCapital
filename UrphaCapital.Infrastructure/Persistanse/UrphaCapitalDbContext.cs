using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.HasherServices;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Infrastructure.Persistanse
{
    public class UrphaCapitalDbContext : DbContext, IApplicationDbContext
    {
        private readonly IPasswordHasher _passwordHasher;
        public UrphaCapitalDbContext(DbContextOptions<UrphaCapitalDbContext> options, IPasswordHasher passwordHasher)
            : base(options)
        {
            Database.Migrate();
            _passwordHasher = passwordHasher;
        }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Homeworks> Homeworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Homeworks>()
    .HasOne(h => h.Lesson)
    .WithMany()
    .HasForeignKey(h => h.LessonId);


            var password = "Admin01!";
            var salt = Guid.NewGuid().ToString();
            var hashedPass = _passwordHasher.Encrypt(password, salt);
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                Id = 1,
                Name = "Ozodali",
                Email = "admin@gmail.com",
                PhoneNumber = "+998934013443",
                Role = "SuperAdmin",
                PasswordHash = hashedPass,
                Salt = salt,
            });

        }

        async ValueTask<int> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
