using ErrorOr;

namespace App.Application.Common.Models.Email;

public static class EmailErrors
{
  public static readonly Error SendFailed = Error.Failure("Email.SendFailed", "Failed to send email");
  public static readonly Error TemplateNotFound = Error.NotFound("Email.TemplateNotFound", "Email template not found");
}