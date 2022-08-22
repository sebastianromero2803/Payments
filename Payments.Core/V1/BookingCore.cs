using AutoMapper;
using Microsoft.Extensions.Logging;
using Payments.Contracts.Repository;
using Payments.Core.Handlers;
using Payments.Entities.DTOs;
using Payments.Entities.Entities;

namespace Payments.Core.V1
{
    public class BookingCore
    {
        private readonly IBookingRepository _context;
        private readonly ErrorHandler<Booking> _errorHandler;
        private readonly ILogger<Booking> _logger;
        private readonly IMapper _mapper;

        public BookingCore(IBookingRepository context, ILogger<Booking> logger, IMapper mapper)
        {
            _context=context;
            _logger=logger;
            _mapper=mapper;
            _errorHandler = new ErrorHandler<Booking>(logger);
        }
        public async Task<List<Booking>> AddDetails(int bookingId, List<BookingDto> bookingDetails)
        {
            List<Booking> details = new();
            foreach (var detail in bookingDetails)
            {
                Booking newDetail = _mapper.Map<Booking>(detail);
                newDetail.PaymentId = bookingId;
                var result = await _context.AddAsync(newDetail);
                details.Add(result.Item1);
            }
            return details;
        }

        internal async Task<List<Booking>> GetDetailsByBookingId(int id)
        {
            return await _context.GetByFilterAsync(bd => bd.PaymentId.Equals(id));
        }
    }
}
