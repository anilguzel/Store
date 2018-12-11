using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Store.Business.Concrete;
using Store.DataAccess.Abstract;
using Store.DataAccess.Models;

namespace ProductManagerTests
{
    [testclass]
    public class ProductManagerTests
    {
        [SetUp]
        public void Setup()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager productManager = new ProductManager(mock.Object);

            productManager.Add(new Product());

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}