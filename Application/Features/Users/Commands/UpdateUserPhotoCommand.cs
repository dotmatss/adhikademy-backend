using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public record UpdateUserPhotoCommand(int UserId, string PhotoUrl) : IRequest<bool>;
}
