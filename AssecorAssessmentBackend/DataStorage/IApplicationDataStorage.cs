using AssecorAssessmentBackend.Models;
using LanguageExt;

namespace AssecorAssessmentBackend.Global;

public interface IApplicationDataStorage
{

    public IAsyncEnumerable<Person> GetPeople();
    public Task<Person?> FindPersonById(long id);
    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(uint colorId);
    public IAsyncEnumerable<Person> GetPeopleByFavoriteColor(string name);
    
    public Task<List<Color>> GetColors();
    public Task<List<Color>> GetColors(string searchText);
    
    public Task<Color?> GetColorById(uint id);
    public Task<Color?> GetColorByName(string name);
    
    public Task<Either<Exception, Person>> AddPersonAsync(Person person);
    
    public Task<Color> AddColorAsync(Color color);
    
    public Task<Color[]> AddColorsAsync(Color[] colors);
    
}
