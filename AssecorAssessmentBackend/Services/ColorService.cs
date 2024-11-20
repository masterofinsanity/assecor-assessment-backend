using assecor_assessment_backend.Models;
using System.Linq;
using assecor_assessment_backend.Global;
using Microsoft.EntityFrameworkCore;

namespace assecor_assessment_backend.Services;

public interface IColorService
{
    Task<List<Color>> GetColors(string? searchText = null);
    
    Task<Color?> GetColor(string color);

    Task<Color?> GetColor(uint id);
}

public sealed class ColorService : IColorService
{
    private readonly IApplicationDataStorage _context;

    public ColorService(IApplicationDataStorage context)
    {
        _context = context;
    }

    public async Task<List<Color>> GetColors(string? searchText = null)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return await _context.GetColors();
        }
        
        return await _context.GetColors(searchText);
    }

    public Task<Color?> GetColor(string color)
    {
        return _context.GetColorByName(color);
    }

    public Task<Color?> GetColor(uint id)
    {
        return _context.GetColorById(id);
    }
    
}