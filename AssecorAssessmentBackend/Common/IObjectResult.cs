namespace assecor_assessment_backend.Common;

public interface IObjectResult
{
    public int ResponseStatus { get; }
    
    public object? ResponseBody { get; }
}