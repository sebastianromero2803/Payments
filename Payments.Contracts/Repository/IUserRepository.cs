using Payments.Contracts.Generics;
using Payments.Entities.Entities;

namespace Payments.Contracts.Repository
{
    public interface IUserRepository : IGenericActionDbQuery<User>, IGenericActionDbUpdate<User>
    {
    }
}
