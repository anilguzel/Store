using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public List<Product> Get()
        {
            return _productService.GetList();
        }
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productService.Get(id);
        }

        [HttpPost]
        public Product Add(Product product)
        {
            return _productService.Add(product);
        }

        [HttpPut]
        public Product Update(Product product)
        {
            return _productService.Update(product);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _productService.Delete(_productService.Get(id));
        }
    }
}