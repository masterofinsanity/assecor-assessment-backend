using assecor_assessment_backend.Models;
using LanguageExt;

namespace assecor_assessment_backend.Global;

public interface IApplicationDataStorage
{

    public IAsyncEnumerable<Person> GetPeople();
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
