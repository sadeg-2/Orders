using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using Orders.Infrastructure.Services.Categories;
using Orders.Infrastructure.Services.Resturants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    public class ResturantController : BaseController
    {
        private readonly IReustarantService _resturantService;

        public ResturantController(IReustarantService resturantService)
        {
            _resturantService = resturantService;
        }

        [HttpGet]
        public ActionResult GetAll(string serachkey)
        {
            var categories = _resturantService.GetAll(serachkey);
            return Ok(GetAPIResponse(categories));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var category = _resturantService.NearMe(UserId);
            return Ok(GetAPIResponse(category));

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateResturantDto dto)
        {
            var savedId = _resturantService.Create(dto);
            return Ok(GetAPIResponse(savedId));
        }

        //[HttpPut]
        //public IActionResult Update(UpdateCategoryDto dto)
        //{
        //    var savedId = _resturantService.Update(dto);
        //    return Ok(GetAPIResponse(savedId));
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var deletedId = _resturantService.Delete(id);
        //    return Ok(GetAPIResponse(deletedId));
        //}

    }
}
