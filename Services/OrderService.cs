using ProvaPub.Models;
using ProvaPub.Payments;

namespace ProvaPub.Services
{
	public class OrderService
	{
        private readonly Dictionary<string, IPaymentMethod> _paymentMethods;

        public OrderService()
        {
			_paymentMethods = new Dictionary<string, IPaymentMethod>
			{
				{ "pix", new PixPayment() },
				{ "creditcard", new CreditCardPayment() },
				{ "paypal", new PayPalPayment() }
			};
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            if (_paymentMethods.TryGetValue(paymentMethod.ToLower(), out var paymentMethodHandler))
            {
                return await paymentMethodHandler.Pay(paymentValue, customerId);
            }
            else
            {
                throw new ArgumentException("Método de pagamento não suportado.", nameof(paymentMethod));
            }
        }
    }
}
