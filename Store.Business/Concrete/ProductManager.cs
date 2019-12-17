using System;
using System.Collections.Generic;
using System.Text;
using Store.Business.Abstract;
using Store.DataAccess.Abstract;
using Store.DataAccess.Models;

namespace Store.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        public bool Delete(Product product)
        {
            return _productDal.Delete(product);
        }

        public Product Get(int id)
        {
            return _productDal.Get(x => x.Id == id);
        }

        public List<Product> GetList()
        {
            return _productDal.GetList();
        }

        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
