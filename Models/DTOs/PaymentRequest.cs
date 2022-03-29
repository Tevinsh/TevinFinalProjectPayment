namespace PaymentAPI.Models
{
    public class PaymentRequest
        {
            public string cardOwnerName {get;set;}
            public string cardNumber {get;set;}
            public string expirationDate {get; set;}
            public string securityCode {get; set;}
        }
}