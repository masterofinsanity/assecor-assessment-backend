using assecor_assessment_backend.Global;
using assecor_assessment_backend.Models;
using LanguageExt;
using static Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace assecor_assessment_backend.DataStorage;

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

    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(uint id)
    {
        return _context.People.Where(p => p.ColorId == id).AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(string name)
    {
        return _context.People.Where(p => p.Color != null && p.Color.Name == name).AsAsyncEnumerable();
    }

    public Task<List<Color>> GetFavoriteColors()
    {
        return _context.Colors.ToListAsync();
    }

    public Task<List<Color>> GetColors()
    {
        return _context.Colors.ToListAsync();
    }

    public Task<List<Color>> GetColors(string searchText)
    {
        return _context.Colors.Where(c => c.Name.Contains(searchText)).ToListAsync();
    }

    public Task<Color?> GetColorById(uint id)
    {
        return _context.Colors.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Color?> GetColorByName(string name)
    {
        return _context.Colors.FirstOrDefaultAsync(c => c.Name == name);
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