using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PaymentService.Helper;
using PaymentService.Models;
using Stripe;

namespace PaymentService.Repository
{
    public class MakePayment : IMakePayment
    {
        public async Task<dynamic> PayAsync(PaymentModel payment, string secretKey)
        {
            try
            {
                StripeConfiguration.ApiKey = secretKey;

                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = payment.cardNumber,
                        ExpMonth = payment.expiryMonth,
                        ExpYear = payment.expiryYear,
                        Cvc = payment.cvc
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(tokenOptions);

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = payment.value,
                    Description = "Microservice Test Payment",
                    ReceiptEmail = payment.userEmail,
                    Currency = "eur",
                    Source = stripeToken.Id // the card
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);

                if (charge.Paid)
                    return "Success";
                else
                    return "Failed";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
