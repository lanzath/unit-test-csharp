using Store.Domain.Entities;

namespace Store.Domain.Interfaces;

public interface ICustomerRepository
{
    Customer Get(string document);
}
