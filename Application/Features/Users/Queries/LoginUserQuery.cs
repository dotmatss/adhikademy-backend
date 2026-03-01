using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public record LoginUserQuery(string Email, string Password) : IRequest<string?>; // returns JWT
}
