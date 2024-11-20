using assecor_assessment_backend.Common;

namespace assecor_assessment_backend.Exceptions;

public sealed class FileUnmodifiableException : Exception, IObjectResult
{
    public FileUnmodifiableException(): base("File must not be modified")
    {
        
    }
    
    public int ResponseStatus => StatusCodes.Status403Forbidden;

    public object ResponseBody => Message;
}