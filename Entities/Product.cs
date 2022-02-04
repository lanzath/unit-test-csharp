using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Entities;

public class Product : Entity
{
    public Product(string title, decimal price, bool active)
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsGreaterThan(title, 3, "Title", "Title must have at least three characters")
                .IsGreaterThan(price, 0, "Price", "Price must be greater than zero")
        );

        Title = title;
        Price = price;
        Active = active;
    }

    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
}
