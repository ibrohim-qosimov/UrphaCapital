﻿using MediatR;
using Microsoft.AspNetCore.Http;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Mentors.Commands
{
    public class UpdateMentorCommand : IRequest<ResponseModel>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? Picture { get; set; }
        public string? PasswordHash { get; set; }
    }
}
