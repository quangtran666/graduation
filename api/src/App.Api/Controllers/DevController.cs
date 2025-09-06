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
}
#endif