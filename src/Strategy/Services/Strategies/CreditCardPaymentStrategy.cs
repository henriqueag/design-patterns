using Strategy.Models;

namespace Strategy.Services.Strategies;

internal class CreditCardPaymentStrategy : IPaymentManager
{
    public string PaymentType => "Credit Card";

    public void ProcessPayment(PaymentMethod paymentMethod)
    {
        if (paymentMethod is not CreditCardPayment creditCardPayment)
            throw new ArgumentException("A forma de pagamento não é com cartão de crédito", nameof(paymentMethod));
    }
}