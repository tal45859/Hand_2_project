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
    public class ReportingController : ControllerBase
    {
        //בנאי
        //קבלת דיווח לפי מזהה
        //הוספת דיווח
        //מחיקת דיווח
        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        //קבלת רשימת דיווחים לפי מזהה מודעה
        //קבלת רשימת כל הדיווחים
        //קבלת רשימת הדיווחים הלא פתורים
        //קבלת רשימת הדיווחים הפתורים

        private readonly ReportingService _service;

        //בנאי
        public ReportingController(ReportingService reportingService)
        {
            _service = reportingService;
        }

        //קבלת דיווח לפי מזהה
        [HttpGet,Route("GetReportingById/{Id}")]
        public ActionResult GetReportingById(int Id)
        {
            Reporting reportingFtomDB=_service.GetReportingById(Id);
            if(reportingFtomDB != null)
            {
                return Ok(reportingFtomDB);
            }
            return BadRequest("לא הצלחנו למצוא את הדוח");
        }

        //הוספת דיווח
        [HttpPost,Route("AddReporting"),AllowAnonymous]
        public ActionResult AddReporting([FromBody]ReportingDTO ReportingYoAdd)
        {
            bool isok = _service.AddReporting(ReportingYoAdd);
            if(isok)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את הדוח");
        }

        //מחיקת דיווח
        [HttpDelete,Route("DeleteReportingById/{Id}")]
        public ActionResult DeleteReportingById(int Id)
        {
            ResponseDTO responseDTO = _service.DeleteReporting(Id);
            if(responseDTO.Status==Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        [HttpPut, Route("UpdateReporting")]
        public ActionResult UpdateReporting([FromBody]ReportingDTO ReportingToUpdate)
        {
            ResponseDTO responseDTO = _service.UpdateReporting(ReportingToUpdate);
            if(responseDTO.Status!=Data.DTO.StatusCode.Error)
            {
                return BadRequest(responseDTO.Message);
            }
            return Ok();
        }

        //קבלת רשימת דיווחים לפי מזהה מודעה
        [HttpGet,Route("GetAllReportingByPostId/{Id}")]
        public ActionResult GetAllReportingByPostId(int Id)
        {
            List<Reporting> LReporting= _service.GetAllReporingByPostId(Id);
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת כל הדיווחים
        [HttpGet, Route("GetAllReporting")]
        public ActionResult GetAllReporting()
        {
            List<Reporting> LReporting = _service.GetAllReporting();
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת הדיווחים הלא פתורים
        [HttpGet, Route("GetAllReportingActive")]
        public ActionResult GetAllReportingActive()
        {
            List<Reporting> LReporting = _service.GetAllReportingActive();
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }

        //קבלת רשימת הדיווחים הפתורים
        [HttpGet, Route("GetAllReportingNoActive")]
        public ActionResult GetAllReportingNoActive()
        {
            List<Reporting> LReporting = _service.GetAllReportingNoActive();
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא הצלחנו למצוא את הרשימה המבוקשת");
        }
    }
}
