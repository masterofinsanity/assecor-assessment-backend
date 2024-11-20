using assecor_assessment_backend.Common;

namespace assecor_assessment_backend.Exceptions;

public sealed class EntityNotFoundException<TEntity, TPrimaryKey>: Exception, IObjectResult
{
    private readonly string _entityName;

    private readonly TPrimaryKey _identifier;
    
    public EntityNotFoundException(TPrimaryKey identifier): base()
    {
        _entityName = typeof(TEntity).Name;
        
        _identifier = identifier;
    }
    
    public override string Message => $"Entity {_entityName} with id {_identifier} not found";
    
    public int ResponseStatus => StatusCodes.Status404NotFound;

    public object? ResponseBody => Message;

}