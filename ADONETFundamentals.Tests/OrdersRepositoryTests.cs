using ADONETFundamentals.Models;
using ADONETFundamentals.Repositories;
using ADONETFundamentals.Repositories.ADORepositories;
using ADONETFundamentals.Repositories.EFRepositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADONETFundamentals.Tests
{
    public class OrdersRepositoryTests
    {
        private const string FilteringStatus = "FilteringStatus";

        private const int FilteringYear = 2020;

        private const int FilteringMonth = 8;

        private const string FilteringProductName = "FilteringProductName";

        private List<Product> _testProducts;

        private List<Order> _testOrders;

        /// <summary>
        /// products repository to initialiaze and delete data in tests
        /// </summary>
        private ADONETProductsRepository _setupProductsRepository = new ADONETProductsRepository();

        /// <summary>
        /// orders repository to initialiaze and delete data in tests
        /// </summary>
        private ADONETOrdersRepository _setupOrdersRepository = new ADONETOrdersRepository();

        private static object[] _ordersRepositories =
        {
            new ADONETOrdersRepository(),
            new EFOrdersRepository(new ApplicationContext()),
        };

        [SetUp]
        public void Setup()
        {
            _testProducts = new List<Product>()
            {
                new Product()
                {
                    Name = FilteringProductName,
                    Description = "Description1",
                    Height = 1,
                    Length = 1,
                    Weight = 1,
                    Width = 1,
                },
                new Product()
                {
                    Name = "Product2",
                    Description = "Description2",
                    Height = 2,
                    Length = 2,
                    Weight = 2,
                    Width = 2,
                },
                new Product()
                {
                    Name = "Product3",
                    Description = "Description3",
                    Height = 3,
                    Length = 3,
                    Weight = 3,
                    Width = 3,
                },
            };

            foreach (var product in _testProducts)
            {
                _setupProductsRepository.Create(product);
            }

            _testOrders = new List<Order>()
            {
                new Order()
                {
                    Status = "Status1",
                    CreatedDate = new DateTime(2019,1,1),
                    UpdatedDate = new DateTime(2022,1,1),
                    ProductId = _testProducts[0].Id,
                },
                new Order()
                {
                    Status = "Status2",
                    CreatedDate = new DateTime(2020,1,1),
                    UpdatedDate = new DateTime(2022,1,1),
                    ProductId = _testProducts[0].Id,
                },
                new Order()
                {
                    Status = FilteringStatus,
                    CreatedDate = new DateTime(2020,1,1),
                    UpdatedDate = new DateTime(2022,1,1),
                    ProductId = _testProducts[1].Id,
                },
                new Order()
                {
                    Status = "Status3",
                    CreatedDate = new DateTime(2020,8,1),
                    UpdatedDate = new DateTime(2022,1,1),
                    ProductId = _testProducts[1].Id,
                },
                new Order()
                {
                    Status = FilteringStatus,
                    CreatedDate = new DateTime(2019,8,1),
                    UpdatedDate = new DateTime(2022,1,1),
                    ProductId = _testProducts[2].Id,
                },
            };

            foreach (var order in _testOrders)
            {
                _setupOrdersRepository.Create(order);
            }
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var order in _testOrders)
            {
                _setupOrdersRepository.Delete(order.Id);
            }

            foreach (var product in _testProducts)
            {
                _setupProductsRepository.Delete(product.Id);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetAll_ReturnsAllOrders(IOrdersRepository ordersRepository)
        {
            //Act
            var allOrders = ordersRepository.GetAll();
            //To return only orders from test data
            var result = allOrders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(_testOrders.Count, result.Count());
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetById_FirstOrderId_ReturnsFirstOrder(IOrdersRepository ordersRepository)
        {
            //Setup
            var firstOrder = _testOrders.First();

            //Act
            var result = ordersRepository.GetById(firstOrder.Id);

            //Assert
            AssertOrderProperties(firstOrder, result);
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void Delete_FirstOrder_GottenByIdOrderIsNull(IOrdersRepository ordersRepository)
        {
            //Setup
            var firstOrder = _testOrders.First();

            //Act
            ordersRepository.Delete(firstOrder.Id);
            var result = ordersRepository.GetById(firstOrder.Id);

            //Assert
            Assert.IsNull(result);
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void Update_FirstOrder_GottenByIdOrderIsEqual(IOrdersRepository ordersRepository)
        {
            //Setup
            var firstOrder = _testOrders.First();
            firstOrder.Status = "newStatus";
            firstOrder.CreatedDate = new DateTime(2022,2,2);
            firstOrder.UpdatedDate = new DateTime(2022, 2, 2);
            firstOrder.ProductId = _testProducts.Last().Id;

            //Act
            ordersRepository.Update(firstOrder);
            var result = ordersRepository.GetById(firstOrder.Id);

            //Assert
            AssertOrderProperties(firstOrder, result);
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void Create_NewOrder_GottenByIdOrderIsEqualCreated(IOrdersRepository ordersRepository)
        {
            //Setup
            var newOrder = new Order
            {
                Status = "newStatus",
                CreatedDate = new DateTime(2022, 2, 2),
                UpdatedDate = new DateTime(2022, 2, 2),
                ProductId = _testProducts.Last().Id,
            };

            //Act
            var id = ordersRepository.Create(newOrder);
            var result = ordersRepository.GetById(id);

            //Assert
            AssertOrderProperties(newOrder, result);

            //To be removed in teardown
            _testOrders.Add(newOrder);
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetOrdersByStatus_ReturnsOrdersWithFilteringStatus(IOrdersRepository ordersRepository)
        {
            //Act
            var orders = ordersRepository.GetOrdersByStatus(FilteringStatus);
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(2, result.Count());
            foreach (var order in result)
            {
                Assert.AreEqual(FilteringStatus, order.Status);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetOrdersByProduct_ReturnsOrdersWithFilteringProductName(IOrdersRepository ordersRepository)
        {
            //Act
            var orders = ordersRepository.GetOrdersByProduct(FilteringProductName);
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(2, result.Count());

            var resultProducts = _testProducts.Where(product => result.Any(resultOrder => resultOrder.ProductId == product.Id));
            foreach (var product in resultProducts)
            {
                Assert.AreEqual(FilteringProductName, product.Name);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetOrdersByYear_ReturnsOrdersWithFilteringYear(IOrdersRepository ordersRepository)
        {
            //Act
            var orders = ordersRepository.GetOrdersByYear(FilteringYear);
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(3, result.Count());
            foreach (var order in result)
            {
                Assert.AreEqual(FilteringYear, order.CreatedDate.Year);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void GetOrdersByMonth_ReturnsOrdersWithFilteringMonth(IOrdersRepository ordersRepository)
        {
            //Act
            var orders = ordersRepository.GetOrdersByMonth(FilteringMonth);
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(2, result.Count());
            foreach (var order in result)
            {
                Assert.AreEqual(FilteringMonth, order.CreatedDate.Month);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void DeleteOrdersByMonth_GetAllReturnsOrdersWithoutFilteringMonth(IOrdersRepository ordersRepository)
        {
            //Act
            ordersRepository.DeleteOrdersByMonth(FilteringMonth);
            var orders = ordersRepository.GetAll();
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(3, result.Count());
            foreach (var order in result)
            {
                Assert.AreNotEqual(FilteringMonth, order.CreatedDate.Month);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void DeleteOrdersByYear_GetAllReturnsOrdersWithoutFilteringYear(IOrdersRepository ordersRepository)
        {
            //Act
            ordersRepository.DeleteOrdersByYear(FilteringYear);
            var orders = ordersRepository.GetAll();
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(2, result.Count());
            foreach (var order in result)
            {
                Assert.AreNotEqual(FilteringYear, order.CreatedDate.Year);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void DeleteOrdersByStatus_GetAllReturnsOrdersWithoutFilteringStatus(IOrdersRepository ordersRepository)
        {
            //Act
            ordersRepository.DeleteOrdersByStatus(FilteringStatus);
            var orders = ordersRepository.GetAll();
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(3, result.Count());
            foreach (var order in result)
            {
                Assert.AreNotEqual(FilteringStatus, order.Status);
            }
        }

        [TestCaseSource(nameof(_ordersRepositories))]
        public void DeleteOrdersByProduct_GetAllReturnsOrdersWithoutFilteringProductName(IOrdersRepository ordersRepository)
        {
            //Act
            ordersRepository.DeleteOrdersByProduct(FilteringProductName);
            var orders = ordersRepository.GetAll();
            //To return only orders from test data
            var result = orders.Where(order => _testOrders.Any(testOrder => testOrder.Id == order.Id));

            //Assert
            Assert.AreEqual(3, result.Count());

            var resultProducts = _testProducts.Where(product => result.Any(resultOrder => resultOrder.ProductId == product.Id));
            foreach (var product in resultProducts)
            {
                Assert.AreNotEqual(FilteringProductName, product.Name);
            }
        }

        private void AssertOrderProperties(Order expected, Order actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Status, actual.Status);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.UpdatedDate, actual.UpdatedDate);
            Assert.AreEqual(expected.ProductId, actual.ProductId);
        }
    }
}