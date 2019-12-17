using System;
using System.Collections.Generic;
using System.Text;
using Store.DataAccess.Models;

namespace Store.Business.Abstract
{
    public interface IProductService
    {
        Product Get(int id);
        List<Product> GetList();
        Product Add(Product product);
        Product Update(Product product);
        bool Delete(Product product);
    }
}
