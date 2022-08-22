using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Contracts.Repository;
using Payments.Core.V1;
using Payments.Entities.DTOs;
using Payments.Entities.Entities;

namespace Payments.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentCore _paymentCore;
        private readonly PaymentMethodCore _paymentMethodCore;

        public PaymentController(IPaymentRepository context, IBookingRepository bookingContext, ILogger<Payment> logger,
                                 IMapper mapper, ILogger<Booking> loggerDetails, IPaymentMethodRepository contextPayment,
                                 ILogger<PaymentMethod> loggerPayment, IUserRepository contextUser, ILogger<User> loggerUser)
        {
            _paymentCore = new PaymentCore(context, bookingContext, logger, mapper, loggerDetails, contextPayment, loggerPayment,
                                           contextUser, loggerUser);
            _paymentMethodCore = new PaymentMethodCore(contextPayment, mapper, loggerPayment);
        }

        [HttpGet("Methods/{idUser}")]
        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods(int idUser)
        {
            var response = await _paymentMethodCore.GetAllMethods(idUser);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentShowDto>> GetPayments(int id)
        {
            var response = await _paymentCore.GetPayment(id);
            return StatusCode((int)response.StatusHttp, response);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentShowDto>> NewPayment(PaymentCreateDto newPayment)
        {
            var response = await _paymentCore.NewPayment(newPayment);
            return StatusCode((int) response.StatusHttp, response);
        }
    }
}
