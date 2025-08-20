using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Domain.Entities;

namespace UrphaCapital.Application.UseCases.Users
{
    public class UserCourseService : IUserCourseService
    {
        private readonly IApplicationDbContext _context;

        public UserCourseService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCourse(long userId, int courseId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == userId);

            if (user == null)
            {
                return false; // User not found
            }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                return false; // Course not found
            }
            
            if( _context.UserCourses.Any(x => x.UserId == userId && x.CourseId == courseId))
            {
                return false; // Course already added for this user
            }

            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };

            await _context.UserCourses.AddAsync(userCourse);
            await _context.SaveChangesAsync();

            return true; // Course added successfully
        }

        public async Task<List<Course>> GetUserCourses(long userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var courseIds = _context.UserCourses.Where(x=>x.UserId == userId).Select(x=>x.CourseId).ToList();

            if (courseIds.Count == 0)
            {
                return new List<Course>(); // No courses found for the user
            }

            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();

           return courses;
        }
    }
}
