using AssecorAssessmentBackend.Common;

namespace AssecorAssessmentBackend.Exceptions;

public sealed class FileUnmodifiableException : Exception, IObjectResult
{
    public FileUnmodifiableException(): base("File must not be modified")
    {
        
    }
    
    public int ResponseStatus => StatusCodes.Status403Forbidden;

    public object ResponseBody => Message;
}