using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.WebSockets;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {

                await _productService.Add(productDTO);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _productService.Update(product);


                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _productService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;

            }


        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var product = await _productService.GetProductById(id);
            if(product == null) return NotFound();
            ViewBag.ImageExist = product.Image != null ? true : false;
           
            return View(product);
        }

    }
}
