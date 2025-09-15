using App.Application.Common.Data;
using App.Application.Common.Models;
using App.Application.User.Auth.Configurations;
using App.Application.User.Auth.Constants;
using App.Application.User.Auth.Events;
using App.Application.User.Auth.Services;
using App.Domain.Entities;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

using UserDomain = App.Domain.Entities.User;

namespace App.Application.User.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IPasswordService _passwordService;
  private readonly AuthSettings _authSettings;
  private readonly IMediator _mediator;

  public RegisterCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    IOptions<AuthSettings> authSettings,
    IMediator mediator
  )
  {
    _unitOfWork = unitOfWork;
    _passwordService = passwordService;
    _authSettings = authSettings.Value;
    _mediator = mediator;
  }

  public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    if (await _unitOfWork.Users.ExistsAsync(request.Username, request.Email))
      return Error.Conflict(AuthErrors.User.ALREADY_EXISTS, "User already exists");

    var createdUser = _unitOfWork.Users.Create(new UserDomain
    {
      Username = request.Username,
      Email = request.Email,
      PasswordHash = _passwordService.HashPassword(request.Password),
      EmailVerified = false
    });

    var verificationToken = _unitOfWork.EmailVerificationTokens.Create(new EmailVerificationToken
    {
      User = createdUser,
      Token = Guid.NewGuid().ToString(),
      ExpiresAt = DateTime.UtcNow.AddMinutes(_authSettings.EmailVerificationTokenExpirationMinutes),
      UsedAt = null
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    await _mediator.Publish(new UserRegisteredEvent(
      createdUser.Id,
      createdUser.Email,
      createdUser.Username,
      verificationToken.Token
    ), cancellationToken);

    return new RegisterResult(
      "Registration successful. Please check your email to verify your account.",
      new UserData(createdUser.Id, createdUser.Username, createdUser.Email, createdUser.EmailVerified)
    );
  }
}