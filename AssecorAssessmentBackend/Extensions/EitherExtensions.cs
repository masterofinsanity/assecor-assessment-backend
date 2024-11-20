using assecor_assessment_backend.Common;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace assecor_assessment_backend.Extensions;

public static class EitherExtensions
{
    public static IActionResult ToActionResult<T1, T2>(this Either<T1, T2> either)
    {
        return either.Match<IActionResult>(result =>
        {
            return new OkObjectResult(result);
        }, exception =>
        {
            if (exception is IObjectResult objectResult)
            {
                return new ObjectResult(objectResult.ResponseBody)
                {
                    StatusCode = objectResult.ResponseStatus
                };
            }
            return new BadRequestResult();
        });
    }
}