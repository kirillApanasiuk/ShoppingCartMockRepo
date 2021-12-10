using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingCart.ShoppingCart.interfaces
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds);
        public Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] productCatalogIds);
    }
}
