using Store.DataAccess.Models;
using Store.Infrastructure.Repository;

namespace Store.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
    }
}