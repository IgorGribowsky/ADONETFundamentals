using ADONETFundamentals.Models;
using ADONETFundamentals.Repositories;
using ADONETFundamentals.Repositories.ADORepositories;
using ADONETFundamentals.Repositories.EFRepositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ADONETFundamentals.Tests
{
    public class ProductsRepositoryTests
    {
        private List<Product> _testProducts;

        /// <summary>
        /// products repository to initialiaze and delete data in tests
        /// </summary>
        private ADONETProductsRepository _setupProductsRepository = new ADONETProductsRepository();

        private static object[] _productsRepositories =
        {
            new ADONETProductsRepository(),
            new EFProductsRepository(new ApplicationContext()),
        };

        [SetUp]
        public void Setup()
        {
            _testProducts = new List<Product>()
            {
                new Product()
                {
                    Name = "Product1",
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
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var product in _testProducts)
            {
                _setupProductsRepository.Delete(product.Id);
            }
        }

        [TestCaseSource(nameof(_productsRepositories))]
        public void GetAll_ReturnsAllProducts(IProductsRepository productsRepository)
        {
            //Act
            var allProducts = productsRepository.GetAll();
            //To return only products from test data
            var result = allProducts.Where(product => _testProducts.Any(testProduct => testProduct.Id == product.Id));

            //Assert
            Assert.AreEqual(_testProducts.Count, result.Count());
        }

        [TestCaseSource(nameof(_productsRepositories))]
        public void GetById_FirstProductId_ReturnsFirstProduct(IProductsRepository productsRepository)
        {
            //Setup
            var firstProduct = _testProducts.First();

            //Act
            var result = productsRepository.GetById(firstProduct.Id);

            //Assert
            AssertProductProperties(firstProduct, result);
        }

        [TestCaseSource(nameof(_productsRepositories))]
        public void Delete_FirstProduct_GottenByIdProductIsNull(IProductsRepository productsRepository)
        {
            //Setup
            var firstProduct = _testProducts.First();

            //Act
            productsRepository.Delete(firstProduct.Id);
            var result = productsRepository.GetById(firstProduct.Id);

            //Assert
            Assert.IsNull(result);
        }

        [TestCaseSource(nameof(_productsRepositories))]
        public void Update_FirstProduct_GottenByIdProductIsEqual(IProductsRepository productsRepository)
        {
            //Setup
            var firstProduct = _testProducts.First();
            firstProduct.Name = "newName";
            firstProduct.Description = "newDescription";
            firstProduct.Length += 1;
            firstProduct.Weight += 1;
            firstProduct.Width += 1;
            firstProduct.Height += 1;

            //Act
            productsRepository.Update(firstProduct);
            var result = productsRepository.GetById(firstProduct.Id);

            //Assert
            AssertProductProperties(firstProduct, result);
        }

        [TestCaseSource(nameof(_productsRepositories))]
        public void Create_NewProduct_GottenByIdProductIsEqualCreated(IProductsRepository productsRepository)
        {
            //Setup
            var newProduct = new Product
            {
                Name = "newName",
                Description = "newDescription",
                Length = 1,
                Weight = 1,
                Width = 1,
                Height = 1,
            };

            //Act
            var id = productsRepository.Create(newProduct);
            var result = productsRepository.GetById(id);

            //Assert
            AssertProductProperties(newProduct, result);

            //To be removed in teardown
            _testProducts.Add(newProduct);
        }

        private void AssertProductProperties(Product expected, Product actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected.Weight, actual.Weight);
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);
        }
    }
}