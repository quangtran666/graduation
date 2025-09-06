namespace App.Application.Common.Data;

public interface IUnitOfWork
{
  IUserRepository Users { get; }
  IRefreshTokenRepository RefreshTokens { get; }
  IEmailVerificationTokenRepository EmailVerificationTokens { get; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}