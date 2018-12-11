using System;
using System.Collections.Generic;
using System.Text;
using Store.DataAccess.Models;

namespace Store.Business.Abstract
{
    public interface ICategoryService
    {
        Category Get(int id);
        List<Category> GetList();
        List<Category> GetListByHierarchy(int id);
        List<Category> GetListByParentId(int id);
        Category Add(Category category);
        Category Update(Category category);
        bool Delete(Category category);

    }
}
