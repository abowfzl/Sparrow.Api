using Microsoft.AspNetCore.Mvc.Filters;

namespace Sparrow.API.Results.Wrapping;

public interface IActionResultWrapper
{
    void Wrap(ResultExecutingContext actionResult);
}
