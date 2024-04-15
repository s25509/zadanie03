namespace RestApi.Animals;

public class Animal
{
    public int IdAnimal { get; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
}