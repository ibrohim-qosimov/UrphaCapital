﻿using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.Filters;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Commands
{
    public class CreateMentorCommand : IRequest<ResponseModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Picture { get; set; }

        [Password(minimumLength: 8)]
        public string PasswordHash { get; set; }
    }
}
