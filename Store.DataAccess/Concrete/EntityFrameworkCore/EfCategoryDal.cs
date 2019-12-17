using System;
using System.Collections.Generic;
using System.Text;
using Store.DataAccess.Abstract;
using Store.DataAccess.Models;
using Store.Infrastructure.Repository.EfCore;

namespace Store.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfCategoryDal : EfRepositoryBase<Category, MarketCoreContext>, ICategoryDal
    {
    }
}
