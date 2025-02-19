using AssecorAssessmentBackend.DataSources;
using AssecorAssessmentBackend.Global;
using AssecorAssessmentBackend.Models;

namespace AssecorAssessmentBackend.Extensions;

public static class ImportPeopleWebAppExtensions
{
    
    private static async Task ImportPeopleAsync(WebApplication app)
    {
        var scope = app.Services.CreateAsyncScope();

        var source = scope.ServiceProvider.GetRequiredService<IApplicationDataSource>();
        
        var dataStorage = scope.ServiceProvider.GetRequiredService<IApplicationDataStorage>();

        await foreach (var person in source.AsAsyncEnumerable<Person>())
        {
            await dataStorage.AddPersonAsync(person);
        }
    }

    public static void ImportPeople(this WebApplication app)
    {
        var task = ImportPeopleAsync(app);

        task.Wait();
    }
    
}