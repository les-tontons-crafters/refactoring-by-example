using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Tests.Doubles;
using Xunit;
using static Xunit.Assert;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderCreationUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;
        private readonly OrderCreationUseCase _useCase;

        public OrderCreationUseCaseTest()
        {
            var food = new Category
            {
                Name = "food",
                TaxPercentage = 10m
            };

            _productCatalog = new InMemoryProductCatalog(
                new List<Product>
                {
                    new()
                    {
                        Name = "salad",
                        Price = 3.56m,
                        Category = food
                    },
                    new()
                    {
                        Name = "tomato",
                        Price = 4.65m,
                        Category = food
                    }
                });

            _orderRepository = new TestOrderRepository();

            _useCase = new OrderCreationUseCase(_orderRepository, _productCatalog);
        }


        [Fact]
        public void SellMultipleItems()
        {
            var items = new Dictionary<string, int>()
            {
                {"salad", 2}, {"tomato", 3}
            };

            _useCase.Run(items);

            var insertedOrder = _orderRepository.GetSavedOrder();
            Equal(OrderStatus.Created, insertedOrder.Status);
            Equal(23.20m, insertedOrder.Total);
            Equal(2.13m, insertedOrder.Tax);
            Equal("EUR", insertedOrder.Currency);
            Equal(2, insertedOrder.Items.Count);
            Equal("salad", insertedOrder.Items[0].Product.Name);
            Equal(3.56m, insertedOrder.Items[0].Product.Price);
            Equal(2, insertedOrder.Items[0].Quantity);
            Equal(7.84m, insertedOrder.Items[0].TaxedAmount);
            Equal(0.72m, insertedOrder.Items[0].Tax);
            Equal("tomato", insertedOrder.Items[1].Product.Name);
            Equal(4.65m, insertedOrder.Items[1].Product.Price);
            Equal(3, insertedOrder.Items[1].Quantity);
            Equal(15.36m, insertedOrder.Items[1].TaxedAmount);
            Equal(1.41m, insertedOrder.Items[1].Tax);
        }

        [Fact]
        public void UnknownProduct()
        {
            var items = new Dictionary<string, int>()
            {
                {"unknown product", 0}
            };


            void actionToTest() => _useCase.Run(items);

            Throws<UnknownProductException>(actionToTest);
        }
    }
}