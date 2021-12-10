using System;

namespace ShoppingCart.ShoppingCart
{
    public record Event(long SequenceNumber, DateTimeOffset OccuredAt, string Name, object Content);
}
