
using App.Application.Auth.Configurations;
using App.Application.Auth.Constants;
using App.Application.Auth.Services;
using App.Application.Common.Data;
using App.Application.Common.Models;
using App.Domain.Entities;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IPasswordService _passwordService;
  private readonly AuthSettings _authSettings;

  public RegisterCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    IOptions<AuthSettings> authSettings
  )
  {
    _unitOfWork = unitOfWork;
    _passwordService = passwordService;
    _authSettings = authSettings.Value;
  }

  public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    if (await _unitOfWork.Users.ExistsAsync(request.Username, request.Email))
      return Error.Conflict(AuthErrors.User.ALREADY_EXISTS, "User already exists");

    var createdUser = _unitOfWork.Users.Create(new User
    {
      Username = request.Username,
      Email = request.Email,
      PasswordHash = _passwordService.HashPassword(request.Password),
      EmailVerified = false
    });

    _unitOfWork.EmailVerificationTokens.Create(new EmailVerificationToken
    {
      User = createdUser,
      Token = Guid.NewGuid().ToString(),
      ExpiresAt = DateTime.UtcNow.AddMinutes(_authSettings.EmailVerificationTokenExpirationMinutes),
      UsedAt = null
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new RegisterResult(
      "Registration successful. Please check your email to verify your account.",
      new UserData(createdUser.Id, createdUser.Username, createdUser.Email, createdUser.EmailVerified)
    );
  }
}