using Microsoft.AspNetCore.Mvc;
using Sparrow.Services.Queries;

namespace Sparrow.API.Controllers;

public class SampleController : ApiControllerBase
{

    [HttpGet]
    public async Task<PaymentOrderQueryResponse> PaymentOrders([FromQuery] PaymentOrderQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return result;
    }
}
