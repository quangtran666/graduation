namespace App.Domain.Common;

public interface ISoftDeletable
{
  DateTime? DeletedAt { get; set; }
  int? DeletedBy { get; set; }
}