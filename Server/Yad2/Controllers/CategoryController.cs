using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yad2.Data.DTO;
using Yad2.Data.Entities;
using Yad2.Services;

namespace Yad2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        // בנאי
        // קבלת קטגוריה לפי מזהה
        // קבלת קטגוריה לפי שם קטגוריה
        // קבלת כל הקטגוריות
        // יצירת קטגוריה
        // מחיקת קטגוריה
        // עדכון קטגוריה

        private readonly CategoryService _service;

        // בנאי
        public CategoryController(CategoryService categoryService)
        {
            _service = categoryService;
        }

        // קבלת קטגוריה לפי מזהה
        [HttpGet,Route("GetCategoryById/{Id}"),AllowAnonymous]
        public ActionResult GetCategoryById(int Id)
        {
            Category CategoryFromDB = _service.GetCategoryById(Id);
            if(CategoryFromDB != null)
            {
                return Ok(CategoryFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את הקטגוריה המבוקשת");
        }

        // קבלת קטגוריה לפי שם קטגוריה
        [HttpGet, Route("GetAreaByName/{Name}"),AllowAnonymous]
        public ActionResult GetAreaByName(string Name)
        {
            Category CategoryFromDB= _service.GetCategoryByName(Name);
            if(CategoryFromDB!=null)
            {
                return Ok(CategoryFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את הקטגוריה המבוקשת");
        }

        // קבלת כל הקטגוריות
        [HttpGet,Route("GetAllCategory"),AllowAnonymous]
        public ActionResult GetAllCategory()
        {
            List<Category> LCategory = _service.GetAllCategory();
            if(LCategory!=null)
            {
                return Ok(LCategory);
            }
            return BadRequest("לא הצלחנו למצוא את הקשימה המבוקשת");
        }

        // יצירת קטגוריה
        [HttpPost,Route("AddCategory")]
        public ActionResult AddCategory([FromBody]CategoryDTO CategoryToAddFromUser)
        {
            bool isok = _service.AddCategory(CategoryToAddFromUser);
            if(isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את הקטגוריה");
        }

        // מחיקת קטגוריה
        [HttpDelete,Route("DeleteCategoryById/{Id}")]
        public ActionResult DeleteCategoryById(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteCategory(Id);
            if(responseDTO.Status==Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }    
            return Ok();
        }
        
        // עדכון קטגוריה
        [HttpPut,Route("UpdateCategory")]
        public ActionResult UpdateCategory([FromBody]CategoryDTO CategoryToUpdateFromUser)
        {
            ResponseDTO responseDTO = _service.UpdateCategory(CategoryToUpdateFromUser);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }
    }
}
