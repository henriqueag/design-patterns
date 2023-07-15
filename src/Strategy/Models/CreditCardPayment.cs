namespace Strategy.Models;

internal class CreditCardPayment : PaymentMethod
{
    public int Installments { get; set; }
    public decimal InstallmentsValue { get; set; }
    public string CardNumber { get; set; }
    public string CardExpirationDate { get; set; }
    public string CardSecurityCode { get; set; }
}
