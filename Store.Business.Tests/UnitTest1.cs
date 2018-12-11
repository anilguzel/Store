using Store.DataAccess.Abstract;
using System;
using Moq;
using Store.DataAccess.Models;
using Xunit;
using Store.Business.Concrete;

namespace Store.Business.Tests
{

    public class UnitTest1
    {
        [Fact]
        public void product_add_test()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager productManager = new ProductManager(mock.Object);

            productManager.Add(new Product());
        }
    }
}
