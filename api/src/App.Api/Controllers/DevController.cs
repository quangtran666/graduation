using Microsoft.AspNetCore.Mvc;

using RazorLight;

namespace App.Api.Controllers;

#if DEBUG
[ApiController]
[Route("api/dev")]
public class DevController : ControllerBase
{
  [HttpGet("preview-welcome-email")]
  public async Task<IActionResult> PreviewWelcomeEmail()
  {
    var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "App.Infrastructure", "Templates", "Email", "WelcomeEmail.cshtml");

    var engine = new RazorLightEngineBuilder()
      .UseFileSystemProject(Path.GetDirectoryName(templatePath))
      .UseMemoryCachingProvider()
      .Build();

    var model = new
    {
      Username = "JohnDoe",
      VerificationUrl = "https://example.com/verify?token=abc123",
      ExpiresAt = DateTime.UtcNow.AddHours(24)
    };

    var html = await engine.CompileRenderAsync(Path.GetFileName(templatePath), model);
    return Content(html, "text/html");
  }

  [HttpGet("preview-password-reset-email")]
  public async Task<IActionResult> PreviewPasswordResetEmail()
  {
    var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "App.Infrastructure", "Templates", "Email", "PasswordResetEmail.cshtml");

    var engine = new RazorLightEngineBuilder()
      .UseFileSystemProject(Path.GetDirectoryName(templatePath))
      .UseMemoryCachingProvider()
      .Build();

    var model = new
    {
      Username = "JohnDoe",
      ResetUrl = "https://example.com/reset-password?token=xyz789",
      ExpiresAt = DateTime.UtcNow.AddHours(1)
    };

    var html = await engine.CompileRenderAsync(Path.GetFileName(templatePath), model);
    return Content(html, "text/html");
  }

  [HttpGet("preview-password-reset-completed-email")]
  public async Task<IActionResult> PreviewPasswordResetCompletedEmail()
  {
    var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "App.Infrastructure", "Templates", "Email", "PasswordResetCompletedEmail.cshtml");

    var engine = new RazorLightEngineBuilder()
      .UseFileSystemProject(Path.GetDirectoryName(templatePath))
      .UseMemoryCachingProvider()
      .Build();

    var model = new
    {
      Username = "JohnDoe"
    };

    var html = await engine.CompileRenderAsync(Path.GetFileName(templatePath), model);
    return Content(html, "text/html");
  }
}
#endif