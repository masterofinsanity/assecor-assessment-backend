using AssecorAssessmentBackend.DataSources;
using AssecorAssessmentBackend.Exceptions;
using AssecorAssessmentBackend.Models;
using LanguageExt;

namespace AssecorAssessmentBackend.Global;

public sealed class FileDataStorage : IApplicationDataStorage
{
    private readonly IApplicationDataSource _dataSource;

    private List<Color> _colors;

    public FileDataStorage(IApplicationDataSource dataStorage)
    {
        _dataSource = dataStorage;
        _colors = [];
    }

    private void IncludeColor(Person person)
    {
        if (person.ColorId.HasValue)
        {
            person.Color = _colors.FirstOrDefault(c => person.ColorId.Value == c.Id);
        }
    }
    
    public async IAsyncEnumerable<Person> GetPeople()
    {
        await foreach (var person in _dataSource.AsAsyncEnumerable<Person>())
        {
            IncludeColor(person);
            
            yield return person;
        }
    }

    public async Task<Person?> FindPersonById(long id)
    {
        await foreach (var person in _dataSource.AsAsyncEnumerable<Person>())
        {
            if (person.Id == id)
            {
                return person;
            }
        }

        return null;
    }

    public async IAsyncEnumerable<Person> GetPeopleByFavoriteColor(uint id)
    {
        await foreach (var person in _dataSource.AsAsyncEnumerable<Person>())
        {
            if (person.ColorId.HasValue && person.ColorId.Value == id)
            {
                IncludeColor(person);
                
                yield return person;
            }
        }
    }

    public async IAsyncEnumerable<Person> GetPeopleByFavoriteColor(string name)
    {
        var color = _colors.FirstOrDefault(c => c.Name == name);

        if (color == null)
        {
            throw new NotSupportedException("Color not found!");
        }
        
        await foreach (var person in _dataSource.AsAsyncEnumerable<Person>())
        {
            if (person.ColorId.HasValue && person.ColorId.Value == color.Id)
            {
                IncludeColor(person);
                
                yield return person;
            }
        }
    }

    public Task<List<Color>> GetColors()
    {
        return Task.FromResult(_colors);
    }

    public Task<List<Color>> GetColors(string searchText)
    {
        return Task.FromResult(_colors.Where(c => c.Name.ToLower().Contains(searchText.ToLower())).ToList());
    }

    public Task<Color?> GetColorById(uint id)
    {
        return Task.FromResult(_colors.FirstOrDefault(c => c.Id == id));
    }

    public Task<Color?> GetColorByName(string name)
    {
        return Task.FromResult(_colors.FirstOrDefault(c => c.Name == name));
    }

    public async Task<Either<Exception, Person>> AddPersonAsync(Person person)
    {
        return new FileUnmodifiableException();
    }

    public Task<Color> AddColorAsync(Color color)
    {
        throw new NotImplementedException();
    }

    public Task<Color[]> AddColorsAsync(Color[] colors)
    {
        _colors = colors.ToList();
        return Task.FromResult(colors);
    }
}