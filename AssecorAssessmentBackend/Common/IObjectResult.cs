namespace AssecorAssessmentBackend.Common;

public interface IObjectResult
{
    public int ResponseStatus { get; }
    
    public object? ResponseBody { get; }
}