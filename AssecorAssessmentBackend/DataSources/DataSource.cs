namespace assecor_assessment_backend.DataSources;

public interface IApplicationDataSource
{
    
    IEnumerable<T> GetAll<T>();

    IAsyncEnumerable<T> AsAsyncEnumerable<T>();
    
}