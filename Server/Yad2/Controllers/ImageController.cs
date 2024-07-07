using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Yad2.Data.DTO;
using Yad2.Data.Entities;
using Yad2.Services;

namespace Yad2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Classic")]
    public class ImageController : ControllerBase
    {
        //בנאי
        //קבלת תמונה לפי מזהה
        //קבלת רשימת כל התמונות
        //הוספת תמונה ישירות לתיקיה
        //קבלת רשימת תמונות לפי מזהה מודעה
        //הוספת תמונה חדשה לבסיס נתונים
        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        //מחיקת תמונה לוודא שזה מנהל
        //בדיקת תקינות התמונה

        private readonly ImageService _service;

        //בנאי
        public ImageController(ImageService imageService)
        {
            _service = imageService;
        }

        //קבלת תמונה לפי מזהה
        [HttpGet,Route("GetImageById/{Id}"),AllowAnonymous]
        public ActionResult GetImageById(int Id)
        {
            Image ImageFtomDB = _service.GetImageById(Id);
            if(ImageFtomDB != null)
            {
                return Ok(ImageFtomDB);
            }
            return BadRequest("לא הצלחנו למצוא את התמונה המבוקשת");
        }

        //קבלת רשימת כל התמונות
        [HttpGet, Route("GetAllImage"), AllowAnonymous]
        public ActionResult GetAllImage()
        {
            List<Image> LImageFtomDB = _service.GetAllImage();
            if (LImageFtomDB != null)
            {
                return Ok(LImageFtomDB);
            }
            return BadRequest("לא הצלחנו למצוא את רשימת התמונות המבוקשת");
        }

        //הוספת תמונה ישירות לתיקיה
        [HttpPost, Route("AddImageToFolder"), Authorize(Roles = "Classic"), DisableRequestSizeLimit]
        public IActionResult UploadImageToFolder()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                    var fullPath = Path.Combine(pathToSave, fileName);
                    //string urlToDB = "https://localhost:44391/StaticFiles/Images/StaticFiles/Images/" + fileName.ToString();
                    string urlToDB = "https://localhost:44391/StaticFiles/Images/" + fileName.ToString();

                    if (IsAPhotoFile(fileName))
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //return Ok(urlToDB);
                        return Ok();
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //קבלת רשימת תמונות לפי מזהה מודעה
        [HttpGet, Route("GetAllImageByPostId/{Id}"), AllowAnonymous]
        public ActionResult GetAllImageByRecipeId(int Id)
        {
            List<Image> LImageForClient = _service.GetAllImageByPostId(Id);
            if (LImageForClient != null)
            {
                return Ok(LImageForClient);
            }
            return BadRequest("לא הצלחנו למצוא את התמונה המבוקשת");
        }

        //הוספת תמונה חדשה לבסיס נתונים
        [HttpPost, Route("AddImageToDB"), Authorize(Roles = "Classic")]
        public ActionResult AddImageToDB([FromBody] ImageDTO ImageToAdd)
        {
            bool IsCreated = _service.AddImage(ImageToAdd);
            if (IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו לשמור את התמונה");
        }

        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        [HttpDelete, Route("DeleteImageForUser/{ImageId}"), Authorize(Roles = "Classic")]
        public ActionResult DeleteImageForUser(int ImageId)
        {
            ResponseDTO Response = _service.DeleteImageForUser(ImageId);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.Message);
            }
            return Ok();
        }

        //מחיקת תמונה לוודא שזה מנהל
        [HttpDelete, Route("DeleteImageForAdmin/{ImageId}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteImageForAdmin(int ImageId)
        {
            ResponseDTO Response = _service.DeleteImageForAdmin(ImageId);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.Message);
            }
            return Ok();
        }

        //בדיקת תקינות התמונה
        private bool IsAPhotoFile(string fileName)
        {
            return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }
    }
}
