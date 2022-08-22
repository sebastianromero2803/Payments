using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Contracts.Generics
{
    public interface IGenericActionDbUpdate<T> where T : class
    {
        Task<Tuple<T, bool>> UpdateAsync(T entity);
    }
}
