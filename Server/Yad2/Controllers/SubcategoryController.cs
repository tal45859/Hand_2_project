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
    public class SubcategoryController : ControllerBase
    {
        //בנאי
        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        // הוספת תת קטגוריה
        // עדכון תת קטגוריה
        // מחיקת תת קטגוריה

        private readonly SubcategoryService _service;

        //בנאי
        public SubcategoryController(SubcategoryService subcategoryService)
        {
            _service = subcategoryService;
        }

        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        [HttpGet,Route("GetSubcategoryById/{Id}"),AllowAnonymous]
        public ActionResult GetSubcategoryById(int Id)
        {
            Subcategory SubcategoryFromDb = _service.GetSubcategoryById(Id);
            if(SubcategoryFromDb != null)
            {
                return Ok(SubcategoryFromDb);
            }
            return BadRequest("לא הצלחנו למצוא את התת קטגוריה המבוקשת");
        }

        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        [HttpGet, Route("GetAllSubcategory"), AllowAnonymous]
        public ActionResult GetAllSubcategory()
        {
            List<Subcategory> LSubcategory = _service.GetAllSubcategory();
            if(LSubcategory != null)
            {
                return Ok(LSubcategory);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        [HttpGet, Route("GetSubcategoryByCategoryId/{Id}"), AllowAnonymous]
        public ActionResult GetSubcategoryByCategoryId(int Id)
        {
            Subcategory SubcategoryFromDb = _service.GetSubcategoryByCategoryId(Id);
            if (SubcategoryFromDb != null)
            {
                return Ok(SubcategoryFromDb);
            }
            return BadRequest("לא הצלחנו למצוא את התת קטגוריה המבוקשת");
        }

        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        [HttpGet, Route("GetAllSubcategoryByCategoryId/{Id}"), AllowAnonymous]
        public ActionResult GetAllSubcategoryByCategoryId(int Id)
        {
            List<Subcategory> LSubcategory = _service.GetAllSubcategoryByCategoryId(Id);
            if (LSubcategory != null)
            {
                return Ok(LSubcategory);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        // הוספת תת קטגוריה
        [HttpPost, Route("AddSubcategory")]
        public ActionResult AddSubcategory([FromBody]SubcategoryDTO SubcategoryToAdd)
        {
            bool isok = _service.AddSubCategory(SubcategoryToAdd);
            if(isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את התת קטגוריה");
        }

        // עדכון תת קטגוריה
        [HttpPut,Route("UpdateSubcategory")]
        public ActionResult UpdateSubcategory([FromBody] SubcategoryDTO SubcategoryToUpdate)
        {
            ResponseDTO responseDTO = _service.UpdateSubcategory(SubcategoryToUpdate);
            if (responseDTO.Status == Data.DTO.StatusCode.Error) 
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        // מחיקת תת קטגוריה
        [HttpDelete,Route("DeleteSubcategoryById/{Id}")]
        public ActionResult DeleteSubcategoryById(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteSubcategory(Id);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }
    }
}
