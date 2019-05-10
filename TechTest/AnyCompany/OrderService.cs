using System;

namespace AnyCompany
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void PlaceOrder(Order order, int customerId)
        {
            if (order.Amount == 0)
            {
                throw new ArgumentException("Order Amount cannot be zero.", nameof(order));
            }

            var customer = CustomerRepository.Load(customerId);

            order.VAT = customer.Country == "UK" ? 0.2d : 0;

            orderRepository.Save(order, customer);
        }
    }
}
