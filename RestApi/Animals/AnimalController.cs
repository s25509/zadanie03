using Microsoft.AspNetCore.Mvc;

namespace RestApi.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        //TODO: validate orderBy
        var animals = "All Animals"; //TODO: fetch from DB
        return Ok(animals);
    }
}