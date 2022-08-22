using AutoMapper;
using Microsoft.Extensions.Logging;
using Payments.Contracts.Repository;
using Payments.Core.Handlers;
using Payments.Entities.Entities;

namespace Payments.Core.V1
{
    public class UserCore
    {
        private readonly IUserRepository _context;
        private readonly ErrorHandler<User> _errorHandler;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        private readonly StripeCore _stripe;


        public UserCore(ILogger<User> logger, IMapper mapper, IUserRepository context)
        {
            _logger = logger;
            _mapper = mapper;
            _errorHandler = new ErrorHandler<User>(logger);
            _context = context;
            _stripe = new StripeCore();
        }

        internal async Task<string> GetCustomerTokenById(int userId)
        {
            User user = await _context.GetByIdAsync(userId);
            if (user.Token == null)
            {
                user.Token = _stripe.CreateCustomer($"{user.Name} {user.LastName}", user.Email);
                await _context.UpdateAsync(user);
            }
            return user.Token;
        }

    }
}
