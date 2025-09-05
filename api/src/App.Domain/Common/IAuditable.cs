namespace App.Domain.Common;

public interface IAuditable
{
  int Id { get; set; }
  DateTime CreatedAt { get; set; }
  DateTime? UpdatedAt { get; set; }
}