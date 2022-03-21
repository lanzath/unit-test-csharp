using Store.Domain.Entities;

namespace Store.Domain.Interfaces;

public interface IOrderRepository
{
    void Save(Order order);
}
