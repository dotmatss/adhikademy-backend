using Application.Features.Users.Queries;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, string?>
    {
        private readonly IUserRepository _repo;
        private readonly IJwtService _jwtService;

        public LoginUserHandler(IUserRepository repo, IJwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        public async Task<string?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailAsync(request.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user);
        }
    }
}
