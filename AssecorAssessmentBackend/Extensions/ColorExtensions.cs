using assecor_assessment_backend.Global;
using assecor_assessment_backend.Models;

namespace assecor_assessment_backend.Extensions;

public static class ServiceCollectionColorsExtension
{
    private static readonly Color[] Colors =
    [
        new Color()
        {
            Id = 1,
            Name = "blau"
        },
        new Color()
        {
            Id = 2,
            Name = "grün"
        },
        new Color()
        {
            Id = 3,
            Name = "violett"
        },
        new Color()
        {
            Id = 4,
            Name = "rot"
        },
        new Color()
        {
            Id = 5,
            Name = "gelb"
        },
        new Color()
        {
            Id = 6,
            Name = "türkis"
        },
        new Color()
        {
            Id = 7,
            Name = "weiß"
        }
    ];
    
    private static async Task AddDefaultColorsAsync(WebApplication app)
    {
        var scope = app.Services.CreateAsyncScope();

        var storage = scope.ServiceProvider.GetRequiredService<IApplicationDataStorage>();

        await storage.AddColorsAsync(Colors);
    }

    public static void AddDefaultColors(this WebApplication app)
    {
        var task = AddDefaultColorsAsync(app);

        task.Wait();
    }
    
}