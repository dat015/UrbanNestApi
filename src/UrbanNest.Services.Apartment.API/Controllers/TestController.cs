using Microsoft.AspNetCore.Mvc;

namespace UrbanNest.Services.Apartment.API.Controllers;

[ApiController]
[Route("api/apartments/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Hello from Apartment Service (via YARP Gateway)!" });
    }
}