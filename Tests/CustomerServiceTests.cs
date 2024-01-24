using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using System.Linq.Expressions;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<TestDbContext> _mockContext;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        { 
            _mockContext = new Mock<TestDbContext>();
            _customerService = new CustomerService(_mockContext.Object);
        }


        [Fact]
        public async Task CanPurchase_WithInvalidCustomerId_ThrowsArgumentOutOfRangeException()
        {
            int invalidCustomerId = -1;
            decimal purchaseValue = 50;

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(invalidCustomerId, purchaseValue));
            Assert.Equal("customerId", exception.ParamName);
        }

        [Fact]
        public async Task CanPurchase_WithInvalidPurchaseValue_ThrowsArgumentOutOfRangeException()
        {
            int customerId = 1;
            decimal invalidPurchaseValue = -50; // ou zero, dependendo do que você quer testar

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.CanPurchase(customerId, invalidPurchaseValue));
            Assert.Equal("purchaseValue", exception.ParamName);
        }

        [Fact]
        public async Task CanPurchase_WithNonExistingCustomerId_ThrowsInvalidOperationException()
        {
            int nonExistingCustomerId = 1;
            decimal purchaseValue = 50;

            _mockContext.Setup(ctx => ctx.Customers.FindAsync(nonExistingCustomerId)).ReturnsAsync((Customer)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _customerService.CanPurchase(nonExistingCustomerId, purchaseValue));
            Assert.Contains(nonExistingCustomerId.ToString(), exception.Message);
        }
    }
}
