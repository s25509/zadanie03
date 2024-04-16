using Microsoft.AspNetCore.Mvc;

namespace RestApi.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController(IAnimalRepository repository) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllAnimals([FromQuery] string? orderBy)
    {
        orderBy ??= "Name";
        if (!AnimalRepository.ValidOrderParameters.Contains(orderBy))
        {
            return BadRequest($"Cannot sort by: {orderBy}");
        }

        var animals = repository.FetchAllAnimals(orderBy); //TODO: procure via service
        return Ok(animals);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: make input JSON2 (application/*+json ?)
        var success = repository.CreateAnimal(dto.Name,dto.Description,dto.Category,dto.Area); //TODO: create via service
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpPut("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateAnimal([FromBody] UpdateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: make input JSON
        var success = true; //TODO: add method to repository, maybe return a 404 if Animal not found?
        return success ? StatusCode(StatusCodes.Status200OK) : Conflict();
    }

    [HttpDelete("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAnimal([FromRoute] int idAnimal)
    {
        var success = true; //TODO: add method to repository, maybe return a 404 if Animal not found?
        return success ? StatusCode(StatusCodes.Status200OK) : Conflict();
    }
}