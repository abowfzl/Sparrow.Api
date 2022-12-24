using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Sparrow.API.Results.Models;

namespace Sparrow.API.Results.Wrapping;

public class ObjectActionResultWrapper : IActionResultWrapper
{
    private readonly IWebHostEnvironment _env;

    public ObjectActionResultWrapper(IWebHostEnvironment env)
    {
        _env = env;
    }

    public void Wrap(ResultExecutingContext actionResult)
    {
        if (actionResult.Result is not ObjectResult objectResult)
        {
            throw new ArgumentException($"{nameof(actionResult)} should be ObjectResult!");
        }

        if (objectResult.Value is not SparrowApiResponseBase)
        {
            var response = new SparrowApiResponse(objectResult.Value);

            actionResult.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }
}