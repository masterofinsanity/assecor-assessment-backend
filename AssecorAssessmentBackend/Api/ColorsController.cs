using assecor_assessment_backend.Models;
using assecor_assessment_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace assecor_assessment_backend.Api;

[ApiController]
[Route("[controller]")]
public class ColorsController
{
    
    private readonly IColorService _colorService;

    public ColorsController(IColorService colorService)
    {
        _colorService = colorService;
    }

    [HttpGet]
    public async Task<List<Color>> Get([FromQuery] string? searchText = null)
    {
        return await _colorService.GetColors(searchText);
    }
    
}