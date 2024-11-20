using assecor_assessment_backend.DTO;
using assecor_assessment_backend.Extensions;
using assecor_assessment_backend.Models;
using assecor_assessment_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace assecor_assessment_backend.Api;

[ApiController]
[Route("[controller]")]
public sealed class PersonsController
{

    private readonly IPersonsService _service;

    public PersonsController(IPersonsService service)
    {
        _service = service;
    }

    [HttpGet]
    public IAsyncEnumerable<Person> Get()
    {
        return _service.GetAllAsync();
    }

    [HttpGet("color/{color}")]
    public IAsyncEnumerable<Person> GetByColor(string color)
    {
        return _service.GetByColorAsync(color);
    }

    [HttpGet("color-id/{colorId}")]
    public IAsyncEnumerable<Person> GetByColorId(uint colorId)
    {
        return _service.GetByColorAsync(colorId);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NewPersonDTO person)
    {
        var result = await _service.CreatePersonAsync(person);

        return result.ToActionResult();
    }
    
}