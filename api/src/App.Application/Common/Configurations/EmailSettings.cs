namespace App.Application.Common.Configurations;

public class EmailSettings
{
  public const string SectionName = "Email";
  public SmtpSettings Smtp { get; set; } = new();
  public FromSettings From { get; set; } = new();

  public string BaseUrl { get; set; } = null!;
}

public class SmtpSettings
{
  public string Host { get; set; } = string.Empty;
  public int Port { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public bool EnableSsl { get; set; }
}

public class FromSettings
{
  public string Name { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
}