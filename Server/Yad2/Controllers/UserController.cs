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
    public class UserController : ControllerBase
    {
        //בנאי
        //הזדאות וקבלת תוקן
        // JWT קבלת אוביקט משתמש על פי
        // JWT קבלת מזהה משתמש על פי
        // הוספת משתמש
        // קבלת משתמש לפי מזהה
        // קבלת כל המשתמשים
        // קבלת כל המנהלים מוגבל למנהל
        // קבלת משתמש על פי מייל
        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        // JWT עדכון משתמש על פי
        // JWT מחיקת משתמש על פי 
        // מחיקת משתמש למנהל 
        // עדכון תפקיד למשתמש מוגבל למנהל 
        // האם קיים מייל כזה
        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        //יצירת סיסמה חדשה מוצפנת
        //קבלת כמה אחוז משתמשים יוזרים קלאסים


        private readonly UserService _service;
        //בנאי
        public UserController(UserService userService)
        {
            _service = userService;
        }

        //הזדאות וקבלת תוקן
        [HttpPost, Route("{auth}"), AllowAnonymous]
        public IActionResult Auth([FromBody] AuthRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("יש להזין מייל וסיסמה");
            }
            User UserFoudFromDB = _service.GetUserForLogin(request);
            if (UserFoudFromDB != null)
            {
                string Token = _service.GetToken(UserFoudFromDB.Id.ToString(), UserFoudFromDB.Role);
                return Ok(Token);
            }
            return Unauthorized();
        }


        // JWT קבלת אוביקט משתמש על פי
        [HttpGet, Route("GetUserByToken"), Authorize(Roles = "Admin,Classic")]
        public ActionResult GetUserByToken()
        {
            User UserForUser = _service.GetUserByJWT();
            if (UserForUser != null)
            {
                return Ok(UserForUser);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // JWT קבלת מזהה משתמש על פי
        [HttpGet, Route("GetUserIdByToken"), Authorize(Roles = "Admin,Classic")]
        public ActionResult GetUserIdByToken()
        {
            int Id = _service.GetUserIdByJWT();
            if (Id > 0)
            {
                return Ok(Id);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // הוספת משתמש
        [HttpPost, Route("AddUser"), AllowAnonymous]
        public ActionResult AddUser([FromBody] UserDTO UserToAdd)
        {
            bool isok = _service.AddUser(UserToAdd);
            if (isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את המשתמש");
        }

        // קבלת משתמש לפי מזהה
        [HttpGet, Route("GetUserById/{Id}"), AllowAnonymous]
        public ActionResult GetUserById(int Id)
        {
            User UserFromDb = _service.GetUserById(Id);
            if (UserFromDb != null)
            {
                return Ok(UserFromDb);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // קבלת כל המשתמשים
        [HttpGet, Route("GetAllUsers"), Authorize(Roles = "Admin")]
        public ActionResult GetAllUsers()
        {
            List<User> LUser = _service.GetAllUser();
            if (LUser != null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא הצלחנו למצוא את רשימת היוזרים");
        }

        // קבלת כל המנהלים מוגבל למנהל
        [HttpGet, Route("GetAllAdmin"), Authorize(Roles = "Admin")]
        public ActionResult GetAllAdmin()
        {
            List<User> LUser = _service.GetAllAdmin();
            if (LUser != null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        // קבלת משתמש על פי מייל
        [HttpGet, Route("GetUserByMail/{Mail}"), AllowAnonymous]
        public ActionResult GetUserByMail(string Mail)
        {
            User UserFromDB = _service.GetUserByMail(Mail);
            if (UserFromDB != null)
            {
                return Ok(UserFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        [HttpGet, Route("GetAllUserNotAdmin"), Authorize(Roles = "Admin")]
        public ActionResult GetAllUserNoAdmin()
        {
            List<User> LUser = _service.GetAllUserNotAdmin();
            if (LUser != null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        // JWT עדכון משתמש על פי
        [HttpPut, Route("UpdateUserByJWT")]
        public ActionResult UpdateUserByJWT([FromBody] UserDTO UserToUpdate)
        {
            ResponseDTO responseDTO = _service.UpdateUserFromJWT(UserToUpdate);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        // JWT מחיקת משתמש על פי 
        [HttpDelete, Route("DeleteUserByJWT")]
        public ActionResult DeleteUserByJWT()
        {
            ResponseDTO responseDTO = _service.DeleteUserForUserByJWT();
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        // מחיקת משתמש למנהל 
        [HttpDelete, Route("DeleteUserByIdForAdmin/{Id}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteUserByIdForAdmin(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteUserForAdmin(Id);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        // עדכון תפקיד למשתמש מוגבל למנהל 
        [HttpPut, Route("ChangeUserRoleForAdmin"), Authorize(Roles = "Admin")]
        public ActionResult ChangeUserRoleForAdmin([FromBody] ChangeUserRoleDTO ChangeUserRoleObj)
        {
            ResponseDTO responseDTO = _service.UpdateRoleForAdmin(ChangeUserRoleObj);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        // האם קיים מייל כזה
        [HttpGet, Route("GetHaveUser/{Mail}"), AllowAnonymous]
        public ActionResult GetHaveUser(string Mail)
        {
            bool isok =_service.GetHaveMail(Mail);
            if(isok)
            {
                return BadRequest(isok);
            }
            return Ok(isok);
        }

        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        [HttpGet,Route("ForgotPassword/{Mail}"),AllowAnonymous]
        public ActionResult ForgotPassword(string Mail)
        {
            bool IsHaveNewPassword = _service.ForgotPassword(Mail);
            if (IsHaveNewPassword)
            {
                return Ok(IsHaveNewPassword);
            }
            return BadRequest(IsHaveNewPassword);
        }

        //קבלת כמה אחוז משתמשים יוזרים קלאסים
        [HttpGet,Route("GetAllPrecentageClassicUser"),Authorize(Roles ="Admin")]
        public ActionResult GetAllPrecentageClassicUser()
        {
            int result = _service.GetAllPrecentageClassicUser();
            if(result > 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
