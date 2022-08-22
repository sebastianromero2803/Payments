using Payments.Contracts.Generics;
using Payments.Entities.Entities;

namespace Payments.Contracts.Repository
{
    public interface IPaymentMethodRepository: IGenericActionDbQuery<PaymentMethod>
    {
    }
}
