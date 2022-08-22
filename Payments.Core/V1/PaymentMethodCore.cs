﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Payments.Contracts.Repository;
using Payments.Core.Handlers;
using Payments.Entities.Entities;
using Payments.Entities.Utils;
using System.Net;

namespace Payments.Core.V1
{
    public class PaymentMethodCore
    {

        private readonly IPaymentMethodRepository _context;
        private readonly ErrorHandler<PaymentMethod> _errorHandler;
        private readonly ILogger<PaymentMethod> _logger;
        private readonly IMapper _mapper;

        public PaymentMethodCore(IPaymentMethodRepository context, IMapper mapper, ILogger<PaymentMethod> logger)
        {
            _logger = logger;
            _errorHandler = new ErrorHandler<PaymentMethod>(logger);
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseService<List<PaymentMethod>>> GetAllMethods(int userId)
        {
            try
            {
                var response = await _context.GetByFilterAsync(p => p.UserId.Equals(userId));
                return new ResponseService<List<PaymentMethod>>(false, response == null ? "No records found" : "Payment methods list", HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return _errorHandler.Error(ex, "List of methods", new List<PaymentMethod>());
            }
        }

        public async Task<PaymentMethod> GetById(int id)
        {
            return await _context.GetByIdAsync(id);
        }
    }
}
