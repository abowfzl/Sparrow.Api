using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sparrow.API.Results.Wrapping
{
    public class ActionResultWrapperFactory : IActionResultWrapperFactory
    {
        public IActionResultWrapper CreateFor(ResultExecutingContext actionResult, IWebHostEnvironment env)
        {
            if (actionResult is null)
            {
                throw new ArgumentException();
            }

            // Basically, we treat ObjectResult and JsonResult in a same way
            if (actionResult.Result is ObjectResult)
            {
                return new ObjectActionResultWrapper(env);
            }

            return new NullActionResultWrapper();

        }
    }
}
