using Application.DTOs;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _repo;

        public RegisterUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var created = await _repo.CreateAsync(user);

            return new UserDto(created.Id, created.Name, created.Email, created.ProfilePhotoUrl);
        }
    }
}
