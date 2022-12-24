using MediatR;
using Microsoft.EntityFrameworkCore;
using Sparrow.Core;
using Sparrow.Core.DTOs;
using Sparrow.Core.Pagination;
using Sparrow.Data.Extentios;
using Sparrow.Data;

namespace Sparrow.Services.Queries;

public class PaymentOrderQueryResponse : BasePagingResponse
{
    public PaymentOrderQueryResponse(IPagedList<PaymentOrderDto> paymentOrders, IList<int?> steps)
    {
        Steps = steps;
        PaymentOrders = paymentOrders;
    }

    public IList<int?> Steps { get; set; }

    public IList<PaymentOrderDto> PaymentOrders { get; set; }
}

public class PaymentOrderQuery : BasePagingRequest, IRequest<PaymentOrderQueryResponse>
{
    public Guid? Id { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public int? CurrentStep { get; set; }

    public DateTime? CreatedFrom { get; set; }

    public DateTime? CreatedTo { get; set; }
}

public class PaymentOrderQueryHandler : IRequestHandler<PaymentOrderQuery, PaymentOrderQueryResponse>
{
    private readonly SparrowDbContext _dbContext;

    public PaymentOrderQueryHandler(SparrowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentOrderQueryResponse> Handle(PaymentOrderQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.AbcBankrefundBases.AsNoTracking();

        if (request.Id.HasValue)
            query = query.Where(q => q.AbcBankrefundId == request.Id.Value);

        if (request.CurrentStep.HasValue)
            query = query.Where(q => q.AbcCurrentStep == request.CurrentStep.Value);

        if (!string.IsNullOrWhiteSpace(request.CustomerPhoneNumber))
            query = query.Where(q => q.AbcMobileNumberText.Equals(request.CustomerPhoneNumber));

        if (request.CreatedFrom.HasValue)
            query = query.Where(o => request.CreatedFrom.Value <= o.CreatedOn);

        if (request.CreatedTo.HasValue)
            query = query.Where(o => request.CreatedTo.Value >= o.CreatedOn);

        var paymentOrders = await query
            .OrderByDescending(b => b.CreatedOn)
            .Select(b => new PaymentOrderDto()
            {
                Id = b.AbcBankrefundId,
                CreatedOn = b.CreatedOn,
                OrderIdStr = b.AbcOrderId,
                CustomerName = b.AbcCardOwner,
                CustomerPhoneNumber = b.AbcMobileNumberText,
                CardNumber = b.AbcCardNumber,
                RequestedPrice = b.AbcPayAmount,
                CurrentStep = b.AbcCurrentStep,
            })
            .ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);

        var allSteps = await _dbContext.AbcBankrefundBases.AsNoTracking().Select(br => br.AbcCurrentStep).Distinct().ToListAsync(cancellationToken);

        var result = new PaymentOrderQueryResponse(paymentOrders, allSteps)
        {
            TotalCount = paymentOrders.TotalCount,
            TotalPages = paymentOrders.TotalPages,
        };

        return result;
    }
}
