using assecor_assessment_backend.DataStorage;
using assecor_assessment_backend.Global;

namespace assecor_assessment_backend.Extensions;

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