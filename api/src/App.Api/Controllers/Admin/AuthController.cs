using App.Application.Admin.Auth.Commands.Login;
using App.Contract.Admin.Auth.Requests;
using App.Contract.Admin.Auth.Responses;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;

  public AuthController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest request)
  {
    var command = new LoginCommand(
      request.Email,
      request.Password
    );

    var result = await _mediator.Send(command);

    return result.MatchFirst(
      loginResult => Ok(new LoginResponse(
        loginResult.Message,
        new UserInfo(
          loginResult.User.Id,
          loginResult.User.Username,
          loginResult.User.Email,
          loginResult.User.EmailVerified
        )
      )),
      error => Problem(
        statusCode: error.Type switch
        {
          ErrorType.NotFound => StatusCodes.Status401Unauthorized,
          ErrorType.Validation => StatusCodes.Status401Unauthorized,
          ErrorType.Forbidden => StatusCodes.Status403Forbidden,
          _ => StatusCodes.Status500InternalServerError
        },
        title: error.Description
      )
    );
  }
}