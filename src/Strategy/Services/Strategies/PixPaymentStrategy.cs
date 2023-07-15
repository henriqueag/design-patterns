using Strategy.Models;

namespace Strategy.Services.Strategies;

internal class PixPaymentStrategy : IPaymentManager
{
    public string PaymentType => "Pix";

    public void ProcessPayment(PaymentMethod paymentMethod)
    {
        if (paymentMethod is not PixPayment pixPayment)
            throw new ArgumentException("A forma de pagamento não é com pix", nameof(paymentMethod));
    }
}
