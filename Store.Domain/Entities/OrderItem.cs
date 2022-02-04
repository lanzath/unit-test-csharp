using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem(Product product, int quantity)
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNull(product, "Product", "Invalid product")
                .IsGreaterThan(quantity, 0, "Quantity", "Quantity must be greater than zero")
        );

        Product = product;
        Price = Product != null ? product.Price : 0;
        Quantity = quantity;
    }

    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public decimal Total() => Price * Quantity;
}
