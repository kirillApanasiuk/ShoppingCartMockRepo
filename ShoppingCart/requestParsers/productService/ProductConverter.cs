using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.ShoppingCart.requestParsers.productService
{
    public class ProductConverter
    {
        public async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var jsonSerializerOptions = new JsonSerializerOptions();

            var deserializableResponseStream = httpResponse.Content.ReadAsStreamAsync();

            var products =  await JsonSerializer.DeserializeAsync<List<ProductCatalogProduct>>(await deserializableResponseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            return products.Select(p => new ShoppingCartItem(p.ProductId,p.ProductName,p.ProductDescription,p.Price));
        }

        private record ProductCatalogProduct(int ProductId, string ProductName, string ProductDescription, Money Price);
    }

}
