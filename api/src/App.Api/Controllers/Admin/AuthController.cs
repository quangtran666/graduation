using App.Api.Authorization.Attributes;
using App.Application.Admin.Auth.Commands.Login;
using App.Application.Admin.Auth.Commands.Logout;
using App.Application.Admin.Auth.Commands.RefreshToken;
using App.Application.Admin.Auth.Queries.GetCurrentUser;
using App.Application.Common.Constants;
using App.Contract.Admin.Auth.Requests;
using App.Contract.Admin.Auth.Responses;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
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

  [HttpPost("refresh")]
  public async Task<IActionResult> RefreshToken()
  {
    var command = new RefreshTokenCommand();
    var result = await _mediator.Send(command);

    return result.MatchFirst(
      refreshResult => Ok(new RefreshTokenResponse(
        refreshResult.Message
      )),
      error => Problem(
        statusCode: error.Type switch
        {
          ErrorType.Validation => StatusCodes.Status401Unauthorized,
          ErrorType.Forbidden => StatusCodes.Status403Forbidden,
          _ => StatusCodes.Status500InternalServerError
        },
        title: error.Description
      )
    );
  }

  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    var command = new LogoutCommand();
    var result = await _mediator.Send(command);

    return result.MatchFirst(
      message => Ok(new LogoutResponse(message)),
      error => Problem(
        statusCode: StatusCodes.Status500InternalServerError,
        title: error.Description
      )
    );
  }

  [HttpGet("me")]
  [Authorize]
  [RequireRole(RoleConstants.ADMIN)]
  public async Task<IActionResult> GetCurrentUser()
  {
    var query = new GetCurrentUserQuery();
    var result = await _mediator.Send(query);

    return result.MatchFirst(
      getCurrentUserResult => Ok(new GetCurrentUserResponse(
        getCurrentUserResult.Message,
        new UserInfo(
          getCurrentUserResult.User.Id,
          getCurrentUserResult.User.Username,
          getCurrentUserResult.User.Email,
          getCurrentUserResult.User.EmailVerified
        )
      )),
      error => Problem(
        statusCode: error.Type switch
        {
          ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
          ErrorType.NotFound => StatusCodes.Status404NotFound,
          ErrorType.Validation => StatusCodes.Status400BadRequest,
          _ => StatusCodes.Status500InternalServerError
        },
        title: error.Description
      )
    );
  }
}