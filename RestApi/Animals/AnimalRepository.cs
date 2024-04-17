using System.Data.SqlClient;

namespace RestApi.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);

    public bool CreateAnimal(string name, string? description, string category, string area);
}

public class AnimalRepository(IConfiguration configuration) : IAnimalRepository
{
    public static readonly string[] ValidOrderParameters = ["name", "description", "category", "area"];

    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        // Already checked in Controller, but better safe than sorry
        var safeOrderBy = ValidOrderParameters.Contains(orderBy) ? orderBy : "name";
        var command = new SqlCommand($"SELECT * FROM Animal ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!,
            };
            animals.Add(animal);
        }

        return animals;
    }

    public bool CreateAnimal(string name, string? description, string category, string area)
    {
        using var connection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("INSERT INTO Animal (name, description, category, area) VALUES (@name, @description, @category, @area)", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        if (description != null)
        {
            command.Parameters.AddWithValue("@description", description);
        }
        else
        {
            command.Parameters.AddWithValue("@description", DBNull.Value);
        }
        var affectedRows = command.ExecuteNonQuery();
        
        return affectedRows == 1;
    }
}