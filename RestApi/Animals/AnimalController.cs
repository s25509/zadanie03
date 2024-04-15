using Microsoft.AspNetCore.Mvc;

namespace RestApi.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAllAnimals([FromQuery] string? orderBy)
    {
        orderBy ??= "name";
        string[] validOrderParameters = ["name", "description", "category", "area"];
        if (!validOrderParameters.Contains(orderBy))
        {
            return BadRequest("Cannot sort by: " + orderBy);
        }

        var animals = "All Animals"; //TODO: fetch from DB, order by orderBy ASC
        return Ok(animals);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: make input JSON2 (application/*+json ?)
        var success = true; //TODO: save in DB
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpPut("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateAnimal([FromBody] CreateAnimalDTO dto)
    {
        //TODO: validate dto?
        //TODO: make input JSON
        var success = true; //TODO: update in DB, maybe 404 if not found?
        return success ? StatusCode(StatusCodes.Status200OK) : Conflict();
    }

    [HttpDelete("{idAnimal:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAnimal()
    {
        var success = true; //TODO: delete in DB, maybe 404 if not found?
        return success ? StatusCode(StatusCodes.Status200OK) : Conflict();
    }
}