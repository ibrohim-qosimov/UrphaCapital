using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Domain.Entities.Auth;
using UrphaCapital.Domain.Entities;

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
        public DbSet<Domain.Entities.Student> Students { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
    }
}
