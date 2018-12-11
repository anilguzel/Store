using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Abstract;
using Store.DataAccess.Models;

namespace Store.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public List<Category> Get()
        {
            return _categoryService.GetList();
        }
        [HttpGet("h/{hierarchyId}")]
        public List<Category> GetListByHierarchy(int hierarchyId)
        {
            return _categoryService.GetListByHierarchy(hierarchyId);
        }
        [HttpGet("p/{parentId}")]
        public List<Category> GetListByParentId(int parentId)
        {
            return _categoryService.GetListByParentId(parentId);
        }
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _categoryService.Get(id);
        }
        [HttpPost]
        public Category Add(Category category)
        {
            return _categoryService.Add(category);
        }

        [HttpPut]
        public Category Update(Category category)
        {
            return _categoryService.Update(category);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _categoryService.Delete(_categoryService.Get(id));
        }
    }
}