using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>>Get()
        {
            var products = await _productService.GetProducts();
            return Ok(products ?? []);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>>Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>>Put(int id, ProductDTO product)
        {
            if (id != product.Id) return BadRequest();
            var result = await _productService.Update(product);
            return Ok(result);

        }

        [HttpPost]

        public async Task<ActionResult<ProductDTO>>Post(ProductDTO productDto)
        {
           if(productDto == null) return BadRequest("Invalid Data");
            var product = await _productService.Add(productDto);
            
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound("Product not found");
            await _productService.Delete(id);
            return Ok();
        }

    }
}
