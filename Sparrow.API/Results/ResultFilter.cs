using Microsoft.AspNetCore.Mvc.Filters;
using Sparrow.API.Results.Wrapping;

namespace Sparrow.API.Results
{
    public class ResultFilter : IResultFilter
    {
        private readonly IActionResultWrapperFactory _actionResultWrapperFactory;
        private readonly IWebHostEnvironment _env;

        public ResultFilter(IActionResultWrapperFactory actionResultWrapperFactory, IWebHostEnvironment env)
        {
            _actionResultWrapperFactory = actionResultWrapperFactory;
            _env = env;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Nothing
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _actionResultWrapperFactory.CreateFor(context, _env).Wrap(context);
        }
    }
}
