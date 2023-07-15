namespace Strategy.Models;

internal abstract class PaymentMethod
{
    protected Guid PaymentId { get; set; }
    protected decimal AmountToPay { get; set; }
    protected DateTime PaymentDate { get; set; }
    protected DateTime DueDate { get; set; }
    protected string CardholderName { get; set; }
}
