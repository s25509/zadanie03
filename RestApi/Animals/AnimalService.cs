namespace RestApi.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int idAnimal);
}

public class AnimalService(IAnimalRepository animalRepository) : IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return animalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTO dto)
    {
        return animalRepository.CreateAnimal(dto.Name,dto.Description,dto.Category,dto.Area);
    }

    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO dto)
    {
        return animalRepository.UpdateAnimal(idAnimal, dto.Name,dto.Description,dto.Category,dto.Area);
    }

    public bool DeleteAnimal(int idAnimal)
    {
        return animalRepository.RemoveAnimal(idAnimal);
    }
}