using ProvaPub.Models;

namespace ProvaPub.Payments
{
    public class PayPalPayment : IPaymentMethod
    {
        public async Task<Order> Pay(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order { Value = paymentValue, CustomerId = customerId, OrderDate = DateTime.Now });
        }
    }
}
