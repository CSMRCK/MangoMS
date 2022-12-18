﻿using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            if(resonse!=null && resonse.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(resonse.Result));
            }
            return View(list);
        }
    }
}
