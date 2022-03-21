using Store.Domain.Entities;

namespace Store.Domain.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> Get(IEnumerable<Guid> ids);
}
