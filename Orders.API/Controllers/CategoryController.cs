using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using Orders.Infrastructure.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult GetAll(string serachkey)
        {
           var categories = _categoryService.GetAll(serachkey);
           return Ok(GetAPIResponse(categories));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var category = _categoryService.Get(id);
            return Ok(GetAPIResponse(category));

        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateCategoryDto dto)
        {
            var savedId = _categoryService.Create(dto);
            return Ok(GetAPIResponse(savedId));
        }

        [HttpPut]
        public IActionResult Update(UpdateCategoryDto dto)
        {
            var savedId = _categoryService.Update(dto);
            return Ok(GetAPIResponse(savedId));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deletedId = _categoryService.Delete(id);
            return Ok(GetAPIResponse(deletedId));
        }

    }
}
