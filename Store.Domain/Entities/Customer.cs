using Flunt.Notifications;
using Flunt.Validations;

namespace Store.Domain.Entities;

public class Customer : Entity
{
    public Customer(string name, string email)
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsGreaterThan(name, 3, "Name", "Name must have at least three characters")
                .IsEmail(name, "Email", "E-mail invalid")
        );

        Name = name;
        Email = email;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
}
