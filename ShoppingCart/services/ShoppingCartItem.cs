namespace ShoppingCart.ShoppingCart
{
    public record ShoppingCartItem(
        int ProductCatalogueId, string ProductName, string Description, Money Price
        )
    {
        public virtual bool Equals(ShoppingCartItem? item) => item != null && ProductCatalogueId.Equals(item.ProductCatalogueId);

        public override int GetHashCode() => ProductCatalogueId.GetHashCode();

    }
}
