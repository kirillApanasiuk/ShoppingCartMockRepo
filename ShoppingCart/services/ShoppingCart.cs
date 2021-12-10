using ShoppingCart.ShoppingCart.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.ShoppingCart
{
    public class ShoppingCart
    {
        private readonly HashSet<ShoppingCartItem> items = new();
        public int UserId { get; }
        public IEnumerable<ShoppingCartItem> Items => items;
        public ShoppingCart(int userId) => UserId = userId;
        public void AddItems(IEnumerable<ShoppingCartItem> itemsForAdding, IEventStore eventStore)
        {
            foreach (var item in itemsForAdding)
            {
                if (items.Add(item))
                    eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
            }
        }

        public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore)
        {
            items.RemoveWhere(item => productCatalogueIds.Contains(item.ProductCatalogueId));
        }

    }
}
