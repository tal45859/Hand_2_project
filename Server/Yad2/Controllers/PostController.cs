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
    [Authorize(Roles = "Admin,Classic")]
    public class PostController : ControllerBase
    {
        //בנאי
        //JWT הוספת מודעה על פי
        //הוספת צפיה למודעה לפי מזהה מודעה
        //מחיקת מודעה למשתמש ומנהל
        //עדכון מודעה למי שייצר אותו
        //JWT קבלת מזהה מודעה אחרון על פי
        //קבלת מודעה לפי מזהה מודעה עם אויבקט
        //קבלת רשימת כל המודעות עם אוביקטים
        // JWT קבלת רשימת כל המודעות של משתמש למשתמש לפי 
        //קבלת רשימת כל המודעות של משתמש מסוים לפי מזהה משתמש
        //קבלת רשימת כל המודעות לפי מזהה תת קטגוריה
        //קבלת רשימת כל המודעות לפי מזהה קטגוריה
        //קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר   
        //JWT קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        //קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר   
        //JWT קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר של משתמש על פי
        //קבלת אחוזים כמה מודעות התוספו היום/ החודש/ השנה

        private readonly PostService _service;

        //בנאי
        public PostController(PostService service)
        {
            _service = service;
        }

        //JWT הוספת מודעה על פי
        [HttpPost,Route("AddPost"),Authorize(Roles ="Classic")]
        public ActionResult AddPost([FromBody]PostDTO PostToAdd)
        {
            bool isok =_service.AddPost(PostToAdd);
            if(isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את המודעה הרצויה");
        }

        //הוספת צפיה למודעה לפי מזהה מודעה
        [HttpGet,Route("AddViewsToPost/{Id}"),AllowAnonymous]
        public ActionResult AddViewsToPost(int Id)
        {
            ResponseDTO responseDTO = _service.AddViewsToPost(Id);
            if(responseDTO.Status==Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //מחיקת מודעה למשתמש ומנהל
        [HttpDelete,Route("DeletePostById/{Id}")]
        public ActionResult DeletePostById(int Id)
        {
            ResponseDTO responseDTO = _service.DeletePost(Id);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //עדכון מודעה למי שייצר אותו
        [HttpPut,Route("UpdatePost"),Authorize(Roles ="Classic")]
        public ActionResult UpdatePost([FromBody]PostDTO PostToUpdate)
        {
            ResponseDTO responseDTO = _service.UpdatePost(PostToUpdate);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //JWT קבלת מזהה מודעה אחרון על פי
        [HttpGet,Route("GetLastPostIdByJWTForUser"),Authorize(Roles ="Classic")]
        public ActionResult GetLastPostIdByJWTForUser()
        {
            int Id = _service.GetLastPostIdByJWT();
            if(Id > 0)
            {
                return Ok(Id);
            }
            return BadRequest("לא הצלחנו למצוא את המודעה המבוקשת");
        }

        //קבלת מודעה לפי מזהה מודעה עם אויבקט
        [HttpGet,Route("GetPostById/{Id}"),AllowAnonymous]
        public ActionResult GetPostById(int Id)
        {
            Post PostFromDB = _service.GetPostById(Id);
            if(PostFromDB != null)
            {
                return Ok(PostFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את המודעה הרצויה");
        }

        //קבלת רשימת כל המודעות עם אוביקטים
        [HttpGet,Route("GetAllPost"),AllowAnonymous]
        public ActionResult GetAllPost()
        {
            List<Post> LPost = _service.GetAllPost();
            if(LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        // JWT קבלת רשימת כל המודעות של משתמש למשתמש לפי 
        [HttpGet,Route("GetAllPostByJWT"),Authorize(Roles = "Classic")]
        public ActionResult GetAllPostByJWT()
        {
            List<Post> LPost = _service.GetAllPostByJWTFoUser();
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת כל המודעות של משתמש מסוים לפי מזהה משתמש
        [HttpGet,Route("GetAllPostByUserId/{Id}"),AllowAnonymous]
        public ActionResult GetAllPostByUserId(int Id)
        {
            List<Post> LPost = _service.GetAllPostByUserId(Id);
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת כל המודעות לפי מזהה תת קטגוריה
        [HttpGet, Route("GetAllPostBySubcategoryId/{Id}"), AllowAnonymous]
        public ActionResult GetAllPostBySubcategoryId(int Id)
        {
            List<Post> LPost = _service.GetAllPostBySubcategoryId(Id);
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת כל המודעות לפי מזהה קטגוריה
        [HttpGet, Route("GetAllPostByCategoryId/{Id}"), AllowAnonymous]
        public ActionResult GetAllPostByCategoryId(int Id)
        {
            List<Post> LPost = _service.GetAllPostByCategoryId(Id);
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר
        [HttpGet, Route("GetAllPostByNumberOfViews"), AllowAnonymous]
        public ActionResult GetAllPostByNumberOfViews()
        {
            List<Post> LPost = _service.GetAllPostByNumberOfViews();
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //JWT קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        [HttpGet, Route("GetAllPostByNumberOfViewsByJWTForUser"), Authorize(Roles ="Classic")]
        public ActionResult GetAllPostByNumberOfViewsByJWTForUser()
        {
            List<Post> LPost = _service.GetAllPostByNumberOfViewsByJWTForUser();
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר   
        [HttpGet, Route("GetAllPostByUploadDate"), AllowAnonymous]
        public ActionResult GetAllPostByUploadDate()
        {
            List<Post> LPost = _service.GetAllPostByUploadDate();
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //JWT קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר של משתמש על פי
        [HttpGet, Route("GetAllPostByUploadDateByJWTForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllPostByUploadDateByJWTForUser()
        {
            List<Post> LPost = _service.GetAllPostByUploadDateByJWTForUser();
            if (LPost != null)
            {
                return Ok(LPost);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת אחוזים כמה מודעות התוספו היום/ החודש/ השנה
        [HttpGet, Route("GetAllPrecentagePostUploadByDate/{RequestDate}"), Authorize(Roles = "Admin")]
        public ActionResult GetAllPrecentageLoginHistoryUserByDate(string RequestDate)
        {
            int result = _service.GetAllPrecentagePostUploadByDate(RequestDate);
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
