using Microsoft.EntityFrameworkCore;
using SB.Common;
using UrphaCapital.Application.Abstractions;
using UrphaCapital.Application.ExternalServices.AuthServices;
using UrphaCapital.Application.ExternalServices.HasherServices;
using UrphaCapital.Application.ViewModels.AuthModels;
using UrphaCapital.Domain.Entities;
using UrphaCapital.Domain.Entities.Auth;

namespace UrphaCapital.Application.UseCases.Users
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;

        public UserService(IApplicationDbContext context, IPasswordHasher passwordHasher, IAuthService authService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authService = authService;
        }

        public async Task<TokenModel> Login(LoginModel login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            var isPasswordValid = _passwordHasher.Verify(login.Password, user.PasswordHash, user.Salt);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            var token = _authService.GenerateToken(user);

            return token;
        }
        public async Task<Response<UserModel>> RegisterUser(RegisterModel dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = EUserRole.Student, // Default role for registration
            };

            var salt = Guid.NewGuid();
            user.Salt = salt.ToString();
            user.PasswordHash = _passwordHasher.Encrypt(dto.Password, user.Salt);

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Courses = new List<Course>()
            };
        }

        public async Task<Response<UserModel>> RegisterMentor(RegisterModel dto)
        {
            var mentor = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = EUserRole.Mentor, // Default role for registration
            };
            var salt = Guid.NewGuid();
            mentor.Salt = salt.ToString();

            mentor.PasswordHash = _passwordHasher.Encrypt(dto.Password, mentor.Salt);

            _context.Users.Add(mentor);
            await _context.SaveChangesAsync();

            return new UserModel
            {
                Id = mentor.Id,
                Name = mentor.Name,
                Email = mentor.Email,
                PhoneNumber = mentor.PhoneNumber,
                Courses = new List<Course>()
            };
        }

        public async Task<Response<UserModel>> GetUserById(long id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return new ResponseError()
                {
                    ErrorCode = 404,
                    ErrorMessage = "User not found."
                };
            }

            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Courses = new List<Course>()
            };
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            return users.Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Courses = new List<Course>()
            });
        }

    }
}
