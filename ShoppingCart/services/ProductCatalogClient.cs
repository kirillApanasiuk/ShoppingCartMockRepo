using ShoppingCart.ShoppingCart.interfaces;
using ShoppingCart.ShoppingCart.requestParsers.productService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShoppingCart.ShoppingCart.services
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private static string productCatalogBaseUrl = @"https://git.io/JeHiE";
        private static string getProductPathTemplate = "?productIds=[{0}]";
        private readonly HttpClient _client;
        private readonly ProductConverter _productConverter;

        public ProductCatalogClient(HttpClient client,ProductConverter productConverter)
        {
            client.BaseAddress = new Uri(productCatalogBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
            _productConverter = productConverter;
        }
        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds)
        {
            var httpResponse = await RequestProductFromProductCatalog(productCatalogIds);
            var items = await _productConverter.ConvertToShoppingCartItems(httpResponse);

            return items;
        }

        public async Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] productCatalogIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join(",", productCatalogIds));

            var result = await _client.GetAsync(productsResource);

            return result;
        }
    }
}
