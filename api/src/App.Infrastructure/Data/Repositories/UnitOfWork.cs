
using App.Application.Common.Data;

namespace App.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _context;
  private IUserRepository? _userRepository;
  private IRefreshTokenRepository? _refreshTokenRepository;
  private IEmailVerificationTokenRepository? _emailVerificationTokenRepository;
  private IPasswordResetTokenRepository? _passwordResetTokenRepository;

  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }

  public IUserRepository Users => _userRepository ??= new UserRepository(_context);

  public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

  public IEmailVerificationTokenRepository EmailVerificationTokens => _emailVerificationTokenRepository ??= new EmailVerificationTokenRepository(_context);

  public IPasswordResetTokenRepository PasswordResetTokens => _passwordResetTokenRepository ??= new PasswordResetTokenRepository(_context);

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await _context.SaveChangesAsync(cancellationToken);
  }
}