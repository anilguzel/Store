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

        [Fact]
        public void product_update_test()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager productManager = new ProductManager(mock.Object);

            productManager.Update(new Product());
        }

        [Fact]
        public void product_delete_test()
        {
            Mock<IProductDal> mock = new Mock<IProductDal>();
            ProductManager productManager = new ProductManager(mock.Object);

            productManager.Delete(new Product());
        }

        [Fact]
        public void category_add_test()
        {
            Mock<ICategoryDal> mock = new Mock<ICategoryDal>();
            CategoryManager categoryManager = new CategoryManager(mock.Object);

            categoryManager.Add(new Category());
        }

        [Fact]
        public void category_update_test()
        {
            Mock<ICategoryDal> mock = new Mock<ICategoryDal>();
            CategoryManager categoryManager = new CategoryManager(mock.Object);

            categoryManager.Update(new Category());
        }

        [Fact]
        public void category_delete_test()
        {
            Mock<ICategoryDal> mock = new Mock<ICategoryDal>();
            CategoryManager categoryManager = new CategoryManager(mock.Object);

            categoryManager.Delete(new Category());
        }


    }
}
