using Strategy.Models;

namespace Strategy.Services;

internal interface IPaymentManager
{
    string PaymentType { get; }

    void ProcessPayment(PaymentMethod paymentMethod);
}
