using Microsoft.AspNetCore.Mvc.Filters;

namespace Sparrow.API.Results.Wrapping;

public class NullActionResultWrapper : IActionResultWrapper
{
    public void Wrap(ResultExecutingContext actionResult)
    {
        // Nothing as it's name suggests
    }
}
