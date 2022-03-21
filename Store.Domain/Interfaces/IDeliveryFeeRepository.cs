namespace Store.Domain.Interfaces;

public interface IDeliveryFeeRepository
{
    decimal Get(string zipcode);
}
