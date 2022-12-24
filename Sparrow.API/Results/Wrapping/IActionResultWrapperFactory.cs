using Microsoft.AspNetCore.Mvc.Filters;

namespace Sparrow.API.Results.Wrapping;

public interface IActionResultWrapperFactory
{
    IActionResultWrapper CreateFor(ResultExecutingContext actionResult, IWebHostEnvironment env);
}
