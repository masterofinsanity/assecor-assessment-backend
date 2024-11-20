using AssecorAssessmentBackend.Global;
using AssecorAssessmentBackend.Models;
using LanguageExt;
using static Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace AssecorAssessmentBackend.DataStorage;

public sealed class DatabaseDataStorage : IApplicationDataStorage
{
    private readonly ApplicationDbContext _context;

    public DatabaseDataStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<Person> GetPeople()
    {
        await foreach (var person in _context.People.AsAsyncEnumerable())
        {
            yield return person;
        }
    }

    public async Task<Person?> FindPersonById(long id)
    {
        return await _context.People
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(uint id)
    {
        return _context.People
            .AsNoTracking()
            .Where(p => p.ColorId == id)
            .AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(string name)
    {
        return _context.People
            .AsNoTracking()
            .Where(p => p.Color != null && p.Color.Name == name)
            .AsAsyncEnumerable();
    }

    public Task<List<Color>> GetFavoriteColors()
    {
        return _context.Colors
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<Color>> GetColors()
    {
        return _context.Colors
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<Color>> GetColors(string searchText)
    {
        return _context.Colors
            .AsNoTracking()
            .Where(c => c.Name.Contains(searchText)).ToListAsync();
    }

    public Task<Color?> GetColorById(uint id)
    {
        return _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Color?> GetColorByName(string name)
    {
        return _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<Either<Exception, Person>> AddPersonAsync(Person person)
    {
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();

        return person;
    }

    public async Task<Color> AddColorAsync(Color color)
    {
        await _context.Colors.AddAsync(color);
        await _context.SaveChangesAsync();

        return color;
    }

    public async Task<Color[]> AddColorsAsync(Color[] colors)
    {
        await _context.Colors.AddRangeAsync(colors);
        await _context.SaveChangesAsync();

        return colors;
    }

}