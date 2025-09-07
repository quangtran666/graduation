using App.Application.Auth.Commands.Register;
using App.Application.Auth.Commands.ResendEmailVerification;
using App.Contract.Auth.Requests;
using App.Contract.Auth.Responses;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;

  public AuthController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest request)
  {
    var command = new RegisterCommand(
      request.Username,
      request.Email,
      request.Password,
      request.ConfirmPassword
    );

    var result = await _mediator.Send(command);

    return result.MatchFirst(
      registerResult => Ok(new RegisterResponse(
        registerResult.Message,
        new UserInfo(
          registerResult.User.Id,
          registerResult.User.Username,
          registerResult.User.Email,
          registerResult.User.EmailVerified
        )
      )),
      error => Problem(
        statusCode: error.Type switch
        {
          ErrorType.Conflict => StatusCodes.Status409Conflict,
          ErrorType.Validation => StatusCodes.Status400BadRequest,
          _ => StatusCodes.Status500InternalServerError
        },
        title: error.Description
      )
    );
  }

  [HttpPost("resend-verification")]
  public async Task<IActionResult> ResendEmailVerification([FromBody] ResendEmailVerificationRequest request)
  {
    var command = new ResendEmailVerificationCommand(request.Email);
    var result = await _mediator.Send(command);

    return result.MatchFirst(
      resendResult => Ok(new ResendEmailVerificationResponse(
        resendResult.Message,
        resendResult.CooldownSeconds
      )),
      error => Problem(
        statusCode: error.Type switch
        {
          ErrorType.NotFound => StatusCodes.Status404NotFound,
          ErrorType.Conflict => StatusCodes.Status409Conflict,
          ErrorType.Validation => StatusCodes.Status400BadRequest,
          _ => StatusCodes.Status500InternalServerError
        },
        title: error.Description
      )
    );
  }
}