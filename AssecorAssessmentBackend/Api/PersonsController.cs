using AssecorAssessmentBackend.DTO;
using AssecorAssessmentBackend.Extensions;
using AssecorAssessmentBackend.Models;
using AssecorAssessmentBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssecorAssessmentBackend.Api;

[ApiController]
[Route("[controller]")]
public sealed class PersonsController : ControllerBase
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var person = await _service.FindByIdAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
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