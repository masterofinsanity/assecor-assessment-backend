using AssecorAssessmentBackend.Models;
using AssecorAssessmentBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssecorAssessmentBackend.Api;

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