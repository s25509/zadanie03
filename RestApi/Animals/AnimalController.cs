using Microsoft.AspNetCore.Mvc;

namespace RestApi.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController(IAnimalService service) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllAnimals([FromQuery] string? orderBy)
    {
        orderBy ??= "name";
        if (!AnimalRepository.ValidOrderParameters.Contains(orderBy))
        {
            return BadRequest($"Cannot sort by: {orderBy}");
        }

        var animals = service.GetAllAnimals(orderBy);
        return Ok(animals);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: check input is JSON2 (application/*+json ??)
        var success = service.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created, dto) : Conflict();
    }

    [HttpPut("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateAnimal([FromRoute] int idAnimal, [FromBody] UpdateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: check input is JSON
        var success = service.UpdateAnimal(idAnimal, dto);
        return success ? StatusCode(StatusCodes.Status200OK, $"Updated Animal with id: {idAnimal}") : Conflict();
    }

    [HttpDelete("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAnimal([FromRoute] int idAnimal)
    {
        var success = service.DeleteAnimal(idAnimal);
        return success ? StatusCode(StatusCodes.Status200OK, $"Removed Animal with id: {idAnimal}") : Conflict();
    }
}