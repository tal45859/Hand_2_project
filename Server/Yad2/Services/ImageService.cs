using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class ImageService
    {
        //תקציר
        //////////////
        //בנאי
        //קבלת תמונה לפי מזהה
        //קבלת רשימת כל התמונות
        //קבלת רשימת תמונות לפי מזהה מודעה
        //הוספת תמונה חדשה
        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        //מחיקת תמונה לוודא שזה מנהל

        private readonly Yad2DbContext m_db;
        private readonly UserService _UserService;

        public ImageService(Yad2DbContext db, UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת תמונה לפי מזהה
        public Image GetImageById(int ImageId)
        {
            return m_db.Image.Where(i=>i.Id == ImageId).FirstOrDefault();
        }

        //קבלת רשימת כל התמונות
        public List<Image> GetAllImage()
        {
            return m_db.Image.ToList();
        }

        //קבלת רשימת תמונות לפי מזהה מודעה
        public List<Image>GetAllImageByPostId(int PostId)
        {
            return m_db.Image.Where(i => i.PostId == PostId).ToList();
        }

        //הוספת תמונה חדשה
        public bool AddImage(ImageDTO ImageToAddFromUser)
        {
            Image imageToAdd = new Image();
            imageToAdd.Url = "https://localhost:44391/StaticFiles/Images/" +ImageToAddFromUser.Url;
            imageToAdd.PostId= ImageToAddFromUser.PostId;
            imageToAdd.UploadDate = System.DateTime.Now;
            m_db.Image.Add(imageToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        public ResponseDTO DeleteImageForUser(int ImageId)
        {
            Image ImageToDelete =GetImageById(ImageId);
            Post post = m_db.Post.Where(p=>p.Id == ImageToDelete.PostId).FirstOrDefault();
            User user = _UserService.GetUserByJWT();
            if (ImageToDelete==null|| ImageId!=ImageToDelete.Id||post.UserId!=user.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "התמונה לא נמצאה בבסיס הנתונים" };
            }
            string url = "https://localhost:44391/";
            string UrlToDelete = ImageToDelete.Url.Substring(url.Length, ImageToDelete.Url.Length - url.Length);
            var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
            FileInfo file = new FileInfo(PathToDelete);
            try
            {
                file.Delete();
                m_db.Image.Remove(ImageToDelete);
                m_db.SaveChanges();
            }
            catch
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את התמונה" };
            }
            return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };

        }

        //מחיקת תמונה לוודא שזה מנהל
        public ResponseDTO DeleteImageForAdmin(int ImageId)
        {
            Image ImageToDelete = GetImageById(ImageId);
            if (ImageToDelete == null || ImageToDelete.Id != ImageId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "התמונה לא נמצאה בבסיס הנתונים" };
            }
            string url = "https://localhost:44391/";
            string UrlToDelete = ImageToDelete.Url.Substring(url.Length, ImageToDelete.Url.Length - url.Length);
            var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
            FileInfo file = new FileInfo(PathToDelete);
            try
            {
                file.Delete();
                m_db.Image.Remove(ImageToDelete);
                m_db.SaveChanges();
            }
            catch
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את התמונה" };
            }
            return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
        }

    }
}
