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
    public class FavoriteController : ControllerBase
    {
        //בנאי
        //הוספת מועדף
        //מחיקת מועדף
        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        //JWT קבלת רשימת מועדפים לפי 

        private readonly FavoriteService _service;

        //בנאי
        public FavoriteController(FavoriteService favoriteService)
        {
            _service = favoriteService;
        }

        //הוספת מועדף
        [HttpPost,Route("AddFavorite")]
        public ActionResult AddFavorite([FromBody]FavoriteDTO FavoriteToAdd)
        {
            bool isok = _service.AddFavorite(FavoriteToAdd);
            if(isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את המועדף");
        }

        //מחיקת מועדף
        [HttpDelete, Route("DeleteFavoriteById/{Id}")]
        public ActionResult DeleteFavoriteById(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteFavorite(Id);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        [HttpGet, Route("GetFavoriteByFavoriteIdAndJWT/{Id}")]
        public ActionResult GetFavoriteByFavoriteIdAndJWT(int Id)
        {
            Favorite FavoriteFromDB = _service.GetFavoriteByFavoriteIdAndJWT(Id);
            if(FavoriteFromDB != null)
            {
                return Ok(FavoriteFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את המועדף");
        }

        //JWT קבלת רשימת מועדפים לפי 
        [HttpGet, Route("GetAllFavoriteByJWT")]
        public ActionResult GetAllFavoriteByJWT()
        {
            List<Favorite> LFavorite = _service.GetAllFavoriteByJWT();
            if(LFavorite != null)
            {
                return Ok(LFavorite);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }
    }
}
