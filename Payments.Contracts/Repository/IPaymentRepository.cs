﻿using Payments.Contracts.Generics;
using Payments.Entities.Entities;

namespace Payments.Contracts.Repository
{
    public interface IPaymentRepository: IGenericActionDbAdd<Payment>, IGenericActionDbQuery<Payment>, IGenericActionDbUpdate<Payment>
    {
    }
}
