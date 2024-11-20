using AssecorAssessmentBackend.DataStorage;
using AssecorAssessmentBackend.Global;

namespace AssecorAssessmentBackend.Extensions;

public static class DataStorage
{
    /*
    public static void AddCachedDataStorage(this IServiceCollection services)
    {
        services.AddSingleton<IDataStorage, CachedDataStorage>();
    }
    */
    public static void AddFileDataStorage(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDataStorage, FileDataStorage>();
    }

    public static void AddDbDataStorage(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDataStorage, DatabaseDataStorage>();
    }
}