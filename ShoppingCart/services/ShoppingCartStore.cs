using ShoppingCart.ShoppingCart.interfaces;
using System.Collections.Generic;

namespace ShoppingCart.ShoppingCart.services
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private static readonly Dictionary<int, ShoppingCart>
            Database = new Dictionary<int, ShoppingCart>();
        public ShoppingCart Get(int userId)
        {
            return Database.ContainsKey(userId) ? Database.GetValueOrDefault(userId) : new ShoppingCart(userId);
        }

        public void RemoveItems(int[] productIds)
        {
            foreach (var id in productIds)
            {
                Database.Remove(id);
            }
        }

        public void Save(ShoppingCart shoppingCart)
        {
            Database[shoppingCart.UserId] = shoppingCart;
        }
    }
}
