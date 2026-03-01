using Application.Features.Users.Commands;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class UpdateUserPhotoCommandHandler : IRequestHandler<UpdateUserPhotoCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPhotoCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null) return false;

            // Update the domain entity property
            user.ProfilePhotoUrl = request.PhotoUrl;

            // Persist the change via repository
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}
