using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new();
            var resonse = await _productService.GetAllProductsAsync<ResponseDTO>();
            if (resonse != null && resonse.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(resonse.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> ProductEdit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductIndex(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var resonse = await _productService.CreateProductAsync<ResponseDTO>(model);
                if (resonse != null && resonse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var resonse = await _productService.GetProductByIdAsync<ResponseDTO>(productId);
            if (resonse != null && resonse.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(resonse.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var resonse = await _productService.UpdateProductAsync<ResponseDTO>(model);
                if (resonse != null && resonse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var resonse = await _productService.GetProductByIdAsync<ResponseDTO>(productId);
            if (resonse != null && resonse.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(resonse.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var resonse = await _productService.DeleteProductAsync<ResponseDTO>(model.ProductId);
                if (resonse != null && resonse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }
    }
}
