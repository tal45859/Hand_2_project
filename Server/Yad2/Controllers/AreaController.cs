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
    public class AreaController : ControllerBase
    {
        //בנאי       
        //קבלת רשימת כל הערים
        //קבלת עיר לפי מזהה
        //קבלת רשימת ערים לפי מזהה אזור
        //הוספת עיר מוגבל למנהל
        //מחיקת עיר מוגבל למנהל
        //עדכון עיר מוגבל למנהל

        private readonly AreaService _serivce;

        //בנאי       
        public AreaController(AreaService areaService)
        {
            _serivce = areaService;
        }

        //קבלת רשימת כל הערים
        [HttpGet, Route("GetAllArea"),AllowAnonymous]
        public ActionResult GetAllArea()
        {
            List<Area> LArea = _serivce.GetAllArea();
            if(LArea != null)
            {
                return Ok(LArea);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת עיר לפי מזהה
        [HttpGet,Route("GetAreaById/{Id}"),AllowAnonymous]
        public ActionResult GetAreaById(int Id)
        {
            Area AreaFromDB = _serivce.GetAreaById(Id);
            if(AreaFromDB != null)
            {
                return Ok(AreaFromDB);
            }
            return BadRequest("לא הצלחנו למצוא את העיר המבוקשת");
        }

        //קבלת רשימת ערים לפי מזהה אזור
        [HttpGet,Route("GetAllAreaByTopAreaId/{TopAreaId}"),AllowAnonymous]
        public ActionResult GetAllAreaByTopAreaId(int TopAreaId)
        {
            List<Area>LArea=_serivce.GetByTopAreaId(TopAreaId);
            if(LArea != null)
            {
                return Ok(LArea);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //הוספת עיר מוגבל למנהל
        [HttpPost,Route("AddArea")]
        public ActionResult AddArea([FromBody]AreaDTO AreFromUserToAdd)
        {
            bool isok = _serivce.AddArea(AreFromUserToAdd);
            if(isok)
            {
                return Created("",null);
            }
            return BadRequest("לא הצלחנו להוסיף את האזור");
        }

        //מחיקת עיר מוגבל למנהל
        [HttpDelete,Route("DeleteAreaById/{Id}")]
        public ActionResult DeleteAreaById(int Id)
        {
            ResponseDTO responseDTO = _serivce.DeleteArea(Id);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //עדכון עיר מוגבל למנהל
        [HttpPut, Route("UpdateArea")]
        public ActionResult UpdateArea([FromBody] AreaDTO AreaForUpdate)
        {
            ResponseDTO responseDTO = _serivce.UpdateArea(AreaForUpdate);
            if (responseDTO.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

    }
}
