using AssecorAssessmentBackend.DTO;
using AssecorAssessmentBackend.Exceptions;
using AssecorAssessmentBackend.Global;
using AssecorAssessmentBackend.Models;
using LanguageExt;

namespace AssecorAssessmentBackend.Services;

public interface IPersonsService
{
    IAsyncEnumerable<Person> GetAllAsync();

    Task<Person?> FindByIdAsync(long id);

    Task<Either<Exception, Person>> CreatePersonAsync(NewPersonDTO person);

    IAsyncEnumerable<Person> GetByColorAsync(string color);

    IAsyncEnumerable<Person> GetByColorAsync(uint color);
}

public sealed class PersonsService : IPersonsService
{

    private readonly IApplicationDataStorage _applicationDataSource;

    public PersonsService(IApplicationDataStorage applicationDataSource)
    {
        _applicationDataSource = applicationDataSource;
    }

    public IAsyncEnumerable<Person> GetAllAsync()
    {
        return _applicationDataSource.GetPeople();
    }

    public async Task<Person?> FindByIdAsync(long id)
    {
        return await _applicationDataSource.FindPersonById(id);
    }

    public IAsyncEnumerable<Person> GetByColorAsync(string color)
    {
        return _applicationDataSource.GetPeopleByFavoriteColor(color);
    }

    public IAsyncEnumerable<Person> GetByColorAsync(uint id)
    {
        return _applicationDataSource.GetPeopleByFavoriteColor(id);
    }

    public async Task<Either<Exception, Person>> CreatePersonAsync(NewPersonDTO newPerson)
    {
        Person person = (Person)newPerson;
        
        if (newPerson.Color != null)
        {
            var color = await _applicationDataSource.GetColorByName(newPerson.Color);

            if (color == null)
            {
                return new EntityNotFoundException<Color, string?>(newPerson.Color);
            }

            person.ColorId = color.Id;
            person.Color = color;
        }
        
        return await _applicationDataSource.AddPersonAsync(person);
    }

}