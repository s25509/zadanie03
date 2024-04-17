using System.ComponentModel.DataAnnotations;

namespace RestApi.Animals;

public class CreateAnimalDTO
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    [Required]
    [MaxLength(200)]
    public string Area { get; set; }
}

public class UpdateAnimalDTO
{
    [MaxLength(200)]
    public string? Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
    [MaxLength(200)]
    public string? Category { get; set; }
    [MaxLength(200)]
    public string? Area { get; set; }
}