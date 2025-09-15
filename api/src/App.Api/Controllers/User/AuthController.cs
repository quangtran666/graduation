using App.Application.User.Auth.Commands.ForgotPassword;
using App.Application.User.Auth.Commands.Login;
using App.Application.User.Auth.Commands.Logout;
using App.Application.User.Auth.Commands.RefreshToken;
using App.Application.User.Auth.Commands.Register;
using App.Application.User.Auth.Commands.ResendEmailVerification;
using App.Application.User.Auth.Commands.ResetPassword;
using App.Application.User.Auth.Commands.VerifyEmail;
using App.Application.User.Auth.Constants;
using App.Application.User.Auth.Queries.GetCurrentUser;
using App.Contract.User.Auth.Requests;
using App.Contract.User.Auth.Responses;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers.User;

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

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest request)
  {
    var command = new LoginCommand(
      request.UsernameOrEmail,
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
      error => error.Code == AuthErrors.User.EMAIL_NOT_VERIFIED_RESENT
        ? Problem(
            statusCode: StatusCodes.Status403Forbidden,
            title: error.Description,
            detail: AuthConstants.ResponseDetails.EMAIL_NOT_VERIFIED_RESENT
          )
        : Problem(
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

  [HttpPost("verify-email")]
  public async Task<IActionResult> VerifyEmail([FromQuery] string token)
  {
    var command = new VerifyEmailCommand(token);
    var result = await _mediator.Send(command);

    return result.MatchFirst(
        verifyResult => Ok(new VerifyEmailResponse(
          verifyResult.Message,
          new UserInfo(
            verifyResult.User.Id,
            verifyResult.User.Username,
            verifyResult.User.Email,
            verifyResult.User.EmailVerified
          )
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

  [HttpPost("forgot-password")]
  public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
  {
    var command = new ForgotPasswordCommand(request.Email);
    var result = await _mediator.Send(command);

    return result.MatchFirst(
      forgotPasswordResult => Ok(new ForgotPasswordResponse(
        forgotPasswordResult.Message
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

  [HttpPost("reset-password")]
  public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
  {
    var command = new ResetPasswordCommand(
      request.Token,
      request.NewPassword,
      request.ConfirmPassword
    );
    var result = await _mediator.Send(command);

    return result.MatchFirst(
      resetPasswordResult => Ok(new ResetPasswordResponse(
        resetPasswordResult.Message
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

  [HttpGet("me")]
  [Authorize]
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