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
    public class LoginHistoryController : ControllerBase
    {
        //בנאי
        //הוספת היסטוריה
        //מחיקת היסטוריה לפי מזהה היסטוריה
        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //קבלת אחוזים כמה התחברו היום/ החודש/ השנה

        private readonly LoginHistoryService _service;

        //בנאי
        public LoginHistoryController(LoginHistoryService loginHistoryService)
        {
            _service = loginHistoryService;
        }

        //הוספת היסטוריה
        [HttpPost,Route("AddLoginHistory")]
        public ActionResult AddLoginHistory()
        {
            bool isok = _service.AddLoginHistory();
            if (isok)
            {
                return Created("",null);
            }
            return BadRequest("לא הצלחנו להוסיף את הההיסטוריה");
        }

        //מחיקת היסטוריה לפי מזהה היסטוריה
        [HttpDelete,Route("DeleteLoginHistoryById/{Id}"),Authorize(Roles ="Admin")]
        public ActionResult DeleteLoginHistoryById(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteLoginHisstory(Id);
            if (responseDTO.Status==Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        [HttpGet,Route("GetLoginHistoryById/{Id}"),Authorize(Roles ="Admin")]
        public ActionResult GetLoginHistoryById(int Id)
        {
            LoginHistory loginHistoryFromDB=_service.GetLoginHistoryById(Id);
            if (loginHistoryFromDB != null)
            {
                return Ok(loginHistoryFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את ההיסטוריה המבוקשת");
        }

        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        [HttpGet,Route("GetAllLoginHistory"),Authorize(Roles ="Admin")]
        public ActionResult GetAllLoginHistory()
        {
            List<LoginHistory> LLoginHistory = _service.GetAllLoginHistory();
            if(LLoginHistory != null)
            {
                return Ok(LLoginHistory);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        [HttpGet, Route("GetAllLoginHistoryByUserId/{Id}"), Authorize(Roles = "Admin")]
        public ActionResult GetAllLoginHistoryByUserId(int Id)
        {
            List<LoginHistory> LLoginHistory = _service.GetAllLoginHistoryByUserId(Id);
            if(LLoginHistory != null)
            {
                return Ok(LLoginHistory);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        [HttpGet, Route("GetLoginHistoryFilteringByDate/{RequestDate}"), Authorize(Roles = "Admin")]
        public ActionResult GetLoginHistoryFilteringByDate(string RequestDate)
        {
            List<LoginHistory> LLoginHistory = _service.GetLoginHistoryFilteringByDate(RequestDate);
            if(LLoginHistory!=null)
            {
                return Ok(LLoginHistory);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת אחוזים כמה התחברו היום/ החודש/ השנה
        [HttpGet,Route("GetAllPrecentageLoginHistoryUserByDate/{RequestDate}"), Authorize(Roles = "Admin")]
        public ActionResult GetAllPrecentageLoginHistoryUserByDate(string RequestDate)
        {
            int result = _service.GetAllPrecentageLoginHistoryUserByDate(RequestDate);
            if(result>0)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
