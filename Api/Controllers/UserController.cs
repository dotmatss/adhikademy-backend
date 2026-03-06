using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserQuery query)
        {
            var token = await _mediator.Send(query);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }

        [HttpPost("{id}/photo")]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile file, [FromServices] IPhotoService photoService)
        {
            // Validation
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            // Upload to Cloud/Storage
            using var stream = file.OpenReadStream();
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var url = await photoService.UploadProfilePhotoAsync(stream, fileName);

            // Send Command to update DB via MediatR
            var result = await _mediator.Send(new UpdateUserPhotoCommand(id, url));

            return result ? Ok(new { url }) : BadRequest("Could not update profile photo.");
        }
    }
}
