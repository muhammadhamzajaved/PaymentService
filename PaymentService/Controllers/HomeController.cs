using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentService.Helper;
using PaymentService.Models;
using PaymentService.Repository;

namespace PaymentService.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private IMakePayment _makePayment;
        private ValidateUser _validateUser;
        private IConfiguration _configuration;
        private string secretKey;

        public HomeController(IMakePayment makePayment, IConfiguration configuration)
        {
            _makePayment = makePayment;
            _validateUser = new ValidateUser();
            _configuration = configuration;
            secretKey = _configuration.GetValue<string>("StripeKeys:Secretkey");
        }

        [Route("api/paymentservice/pay")]
        public async Task<IActionResult> Payment(PaymentModel payment)
        {
            if (_validateUser.ValidarteParameters(payment))
            {
                string status = await _makePayment.PayAsync(payment, secretKey);
                if (status != "Success")
                    return BadRequest(status);
                return Ok(status);
            }
            else
                return BadRequest("Wrong parameters!");
        }
    }
}
