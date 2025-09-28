using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models;

// EF kräver parameterlös ctor och setbara properties
public class Person
{
    [Key]                  // Primärnyckel, auto-identity i SQLite
    public int Id { get; set; }

    [Required, MinLength(2)]
    public string Name { get; set; } = string.Empty;

    public bool Exists { get; set; }
}
