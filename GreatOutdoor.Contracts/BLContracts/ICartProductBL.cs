using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.GreatOutdoor.Entities;
namespace Capgemini.GreatOutdoor.Contracts
{
    public interface ICartProductBL : IDisposable
    {
        Task<bool> AddCartProductBL(CartProduct newCartProduct);
        Task<CartProduct> GetCartProductByCartIDBL(Guid searchCartID);
        Task<bool> UpdateCartProductBL(CartProduct updateCartProduct);
        Task<bool> DeleteCartProductBL(Guid deleteCartID);

    }
}
