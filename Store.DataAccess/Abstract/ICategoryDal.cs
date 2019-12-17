using System;
using System.Collections.Generic;
using System.Text;
using Store.DataAccess.Models;
using Store.Infrastructure.Repository;

namespace Store.DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>
    {
    }
}
