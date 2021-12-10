﻿using Microsoft.AspNetCore.Mvc;
using ShoppingCart.ShoppingCart.interfaces;
using System.Threading.Tasks;

namespace ShoppingCart.ShoppingCart
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore shoppingCartStore;
        private readonly IProductCatalogClient productCatalogClient;
        private readonly IEventStore eventStore;
        public ShoppingCartController(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalogClient, IEventStore eventStore)
        {
            this.shoppingCartStore = shoppingCartStore;
            this.productCatalogClient = productCatalogClient;
            this.eventStore = eventStore;
        }

        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId) => shoppingCartStore.Get(userId);

        [HttpPost("{userId:int}/items")]
        public async Task<ShoppingCart> Post(int userId, [FromBody] int[] productIds)
        {
            var shoppingCart = shoppingCartStore.Get(userId);

            var shoppingCartItems = await this.productCatalogClient.GetShoppingCartItems(productIds);

            shoppingCart.AddItems(shoppingCartItems, eventStore);

            shoppingCartStore.Save(shoppingCart);

            return shoppingCart;
        }

        [HttpDelete("{userid:int}/items")]
        public ShoppingCart Delete(int userId, [FromBody] int[] productIds)
        {
            var shoppingCart = this.shoppingCartStore.Get(userId);

            shoppingCart.RemoveItems(productIds, this.eventStore);

            this.shoppingCartStore.Save(shoppingCart);
            return shoppingCart;
        }
    }
}
