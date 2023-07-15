using Strategy.Models;

namespace Strategy.Services.Strategies;

internal class CashPaymentStrategy : IPaymentManager
{
    public string PaymentType => "Cash";

    public void ProcessPayment(PaymentMethod paymentMethod)
    {
        if (paymentMethod is not CashPayment cashPayment) 
            throw new ArgumentException("A forma de pagamento não é em dinheiro", nameof(paymentMethod));
    }
}
