using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment
    {
        public int Number { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class BoletoPayment : Payment
    {
        public string Boleto { get; set; }
        public string BoletoNumber { get; set; }
    }

    public class CreditCardPayment : Payment
    {
        //Não armazenar dados do cartão e sim utilizar um gateway de pagamento.
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
    }

    public class PayPalPayment : Payment
    {
        public string TransactionCode { get; set; }
    }
}