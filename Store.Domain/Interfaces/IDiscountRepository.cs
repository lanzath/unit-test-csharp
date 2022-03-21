using Store.Domain.Entities;

namespace Store.Domain.Interfaces;

public interface IDiscountRepository
{
    Customer Get(string code);
}
