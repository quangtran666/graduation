using System.Linq.Expressions;

using App.Domain.Common;
using App.Domain.Entities;
using App.Infrastructure.Data.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IConfiguration _configuration;

  public AppDbContext(
    DbContextOptions<AppDbContext> options,
    IConfiguration configuration
  )
    : base(options)
  {
    _configuration = configuration;
  }

  public DbSet<User> Users { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Permission> Permissions { get; set; }
  public DbSet<UserRole> UserRoles { get; set; }
  public DbSet<UserPermission> UserPermissions { get; set; }
  public DbSet<RolePermission> RolePermissions { get; set; }
  public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
  public DbSet<RefreshToken> RefreshTokens { get; set; }
  public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
  public DbSet<ExternalLogin> ExternalLogins { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    var connectionString = _configuration.GetConnectionString(
      "DefaultConnection"
    );
    optionsBuilder.UseNpgsql(
      connectionString,
      options =>
        options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    );
    optionsBuilder.UseLazyLoadingProxies();
    optionsBuilder.AddInterceptors(new AuditingInterceptor());

    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    ApplyGlobalQueryFilters(modelBuilder);

    base.OnModelCreating(modelBuilder);
  }

  private static void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
  {
    foreach (
      var entityType in modelBuilder
        .Model.GetEntityTypes()
        .Where(et => typeof(ISoftDeletable).IsAssignableFrom(et.ClrType))
        .Select(et => et.ClrType)
    )
    {
      var parameter = Expression.Parameter(entityType, "entity"); // entity
      var property = Expression.Property(parameter, "DeletedAt"); // entity.DeletedAt
      var filter = Expression.Lambda(
        Expression.Equal(property, Expression.Constant(null)),
        parameter
      ); // entity => entity.DeletedAt == null
      modelBuilder.Entity(entityType).HasQueryFilter(filter);
    }
  }
}