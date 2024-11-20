namespace AssecorAssessmentBackend.DataSources;

public interface IApplicationDataSource
{
    
    IEnumerable<T> GetAll<T>();

    IAsyncEnumerable<T> AsAsyncEnumerable<T>();
    
}