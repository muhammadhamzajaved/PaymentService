using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Models
{
    public class PaymentModel
    {
        public string cardNumber { get; set; }
        public int? expiryMonth { get; set; }
        public int? expiryYear { get; set; }
        public string cvc { get; set; }
        public int? value { get; set; }
        public string userEmail { get; set; }
    }
}
