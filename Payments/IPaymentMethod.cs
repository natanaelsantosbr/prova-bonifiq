using ProvaPub.Models;

namespace ProvaPub.Payments
{
    public interface IPaymentMethod
    {
        Task<Order> Pay(decimal paymentValue, int customerId);
    }
}
