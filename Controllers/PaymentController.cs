using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PaymentAPI.Models;
using PaymentAPI.Data;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PaymentDetailsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public PaymentDetailsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPaymentDetails()
        {
            var paymentDetails =_context.Payment.ToList();
            return Ok(paymentDetails);
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentDetailsById(int id)
        {
            var result = _context.Payment.FirstOrDefault(x => x.paymentDetailId == id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostPaymentDetails(PaymentRequest request)
        {
            try{
                _context.Payment.Add(
                new PaymentData{
                    paymentDetailId = 0,
                    cardOwnerName = request.cardOwnerName,
                    cardNumber = request.cardNumber,
                    expirationDate = request.expirationDate,
                    securityCode = request.securityCode
                });
                _context.SaveChanges();
            return Ok(new {StatusCode = 200, keterangan = "Sukses", request});
    
            } catch(Exception err) {
                return BadRequest(new {statusCode = "400", error = err.Message});
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult PutPaymentDetails(int id, PaymentData payment)
        {
            if(ModelState.IsValid)
            {
                if(id != payment.paymentDetailId)
                {
                    return BadRequest();
                }
                var result =  _context.Payment.FirstOrDefault(x => x.paymentDetailId == id);
                if (result == null)
                {
                    return NotFound();
                }

                result.expirationDate = payment.expirationDate;
                result.cardNumber = payment.cardNumber;
                result.cardOwnerName = payment.cardOwnerName;
                result.expirationDate = payment.expirationDate;
                result.securityCode = payment.securityCode;
                _context.SaveChanges();
                return Ok(new {keterangan = "sukses" , queri ="update", payment});

            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentDetails(int id)
        {
            var result = _context.Payment.FirstOrDefault(x => x.paymentDetailId == id);

            if(result == null)
            {
                return NotFound();
            }
                
            _context.Payment.Remove(result);
            _context.SaveChanges();

            return Ok(new {status = "dihapus",result});
        }
    }
}