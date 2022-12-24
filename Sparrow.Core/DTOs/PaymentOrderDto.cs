namespace Sparrow.Core.DTOs;

public class PaymentOrderDto
{
    public Guid Id { get; set; }

    public string? OrderIdStr { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? CardNumber { get; set; }

    public int? RequestedPrice { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CurrentStep { get; set; }
}
