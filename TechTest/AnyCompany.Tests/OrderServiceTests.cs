using NUnit.Framework;
using Moq;

namespace AnyCompany.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService orderService;

        private Mock<IOrderRepository> mockOrderRepository;

        [SetUp]
        public void SetUp()
        {
            mockOrderRepository = new Mock<IOrderRepository>();

            orderService = new OrderService(mockOrderRepository.Object);
        }

        [Test]
        public void PlaceOrder_With0Amount_ThrowsArgumentException()
        {
            // Arrange
            var order = new Order();

            // Act
            var action = new TestDelegate(() => orderService.PlaceOrder(order, 1));

            // Assert
            Assert.That(action, Throws.ArgumentException);
        }
    }
}
