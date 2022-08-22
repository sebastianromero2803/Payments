using AutoMapper;
using Microsoft.Extensions.Logging;
using Payments.Contracts.Repository;
using Payments.Core.Handlers;
using Payments.Entities.DTOs;
using Payments.Entities.Entities;
using Payments.Entities.Utils;
using System.Net;

namespace Payments.Core.V1
{
    public class PaymentCore
    {
        private readonly BookingCore _detailCore;
        private readonly PaymentMethodCore _paymentMethodCore;
        private readonly IPaymentRepository _context;
        private readonly ErrorHandler<Payment> _errorHandler;
        private readonly ILogger<Payment> _logger;
        private readonly UserCore _userCore;
        private readonly StripeCore _stripe;

        public PaymentCore(IPaymentRepository context, IBookingRepository detailsContext, ILogger<Payment> logger,
                           IMapper mapper, ILogger<Booking> loggerDetails, IPaymentMethodRepository paymentContext,
                           ILogger<PaymentMethod> loggerPayment, IUserRepository userContext, ILogger<User> userLogger)
        {
            _context = context;
            _logger = logger;
            _errorHandler = new ErrorHandler<Payment>(logger);
            _detailCore = new BookingCore(detailsContext, loggerDetails, mapper);
            _paymentMethodCore = new PaymentMethodCore(paymentContext, mapper, loggerPayment);
            _userCore = new UserCore(userLogger, mapper, userContext);
            _stripe = new StripeCore();
        }

        public async Task<ResponseService<List<PaymentMethod>>> GetAllMethods(int userId)
        {
            return await _paymentMethodCore.GetAllMethods(userId);
        }

        public async Task<ResponseService<PaymentShowDto>> NewPayment(PaymentCreateDto newPayment)
        {
            try
            {
                int totalAmount = 0;
                foreach (var detail in newPayment.Details)
                {
                    totalAmount += detail.Fee;
                }

                string customerToken = await _userCore.GetCustomerTokenById(newPayment.UserId);
                string paymentToken = await PayBooking(newPayment.PaymentMethodId, totalAmount, customerToken);
                Payment booking = new Payment()
                {
                    Date = newPayment.Date,
                    PaymentToken = paymentToken,
                    UserId = newPayment.UserId
                };

                var response = await _context.AddAsync(booking);

                List<Booking> bookingDetails = await _detailCore.AddDetails(response.Item1.Id, newPayment.Details.ToList());

                PaymentShowDto bookingShow = new PaymentShowDto()
                {
                    Id = response.Item1.Id,
                    Details = bookingDetails.ToArray(),
                    Date = response.Item1.Date,
                    PaymentToken = response.Item1.PaymentToken,
                    UserId = response.Item1.UserId,
                };
                return new ResponseService<PaymentShowDto>(false, response == null ? "No records found" : "User list", HttpStatusCode.OK, bookingShow);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Create booking", new PaymentShowDto());
            }
        }

        public async Task<ResponseService<PaymentShowDto>> GetPayment(int id)
        {
            try
            {
                var result = await _context.GetByIdAsync(id);
                var resultDetails = await _detailCore.GetDetailsByBookingId(id);
                PaymentShowDto bookingShow = new()
                {
                    Id = result.Id,
                    Details = resultDetails.ToArray(),
                    Date = result.Date,
                    PaymentToken = result.PaymentToken,
                    UserId = result.UserId,
                };
                return new ResponseService<PaymentShowDto>(false, "Booking show", HttpStatusCode.OK, bookingShow);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "Show booking", new PaymentShowDto());
            }
        }

        public async Task<string> PayBooking(int paymentMethodId, int total, string customerToken)
        {
            PaymentMethod paymentMethod = await _paymentMethodCore.GetById(paymentMethodId);
            var result = _stripe.PlacePayment(paymentMethod.Token, total, customerToken);
            return result.Id;
        }
    }
}
