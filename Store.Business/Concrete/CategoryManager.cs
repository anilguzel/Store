using System;
using System.Collections.Generic;
using System.Text;
using Store.Business.Abstract;
using Store.DataAccess.Abstract;
using Store.DataAccess.Models;

namespace Store.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public Category Add(Category category)
        {
            return _categoryDal.Add(category);
        }

        public bool Delete(Category category)
        {
            return _categoryDal.Delete(category);
        }

        public Category Get(int id)
        {
            return _categoryDal.Get(x => x.Id == id);
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetList();
        }

        public Category Update(Category category)
        {
            return _categoryDal.Update(category);
        }

        public List<Category> GetListByHierarchy(int id)
        {
            return _categoryDal.GetList(x => x.Hierarchy == id);
        }

        public List<Category> GetListByParentId(int id)
        {
            return _categoryDal.GetList(x => x.ParentId == id);
        }
    }
}
