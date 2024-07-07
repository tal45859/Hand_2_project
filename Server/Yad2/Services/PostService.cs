using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class PostService
    {

        //בנאי
        //קבלת מודעה לפי מזהה מודעה נקי מאוביקטים לשימוש פנימי
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


        private readonly Yad2DbContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public PostService(Yad2DbContext db,UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת מודעה לפי מזהה מודעה נקי מאוביקטים לשימוש פנימי
        public Post GetPostByIdFromDB(int postId)
        {
            return m_db.Post.Where(p=>p.Id == postId).FirstOrDefault();
        }

        //JWT הוספת מודעה על פי
        public bool AddPost(PostDTO PostToAddFromUser)
        {
            Post PostToAddToDB = new Post();
            PostToAddToDB.UserId = _UserService.GetUserIdByJWT();
            PostToAddToDB.SubcategoryId = PostToAddFromUser.SubcategoryId;
            PostToAddToDB.AreaId = PostToAddFromUser.AreaId;
            PostToAddToDB.Title = PostToAddFromUser.Title;
            PostToAddToDB.Body = PostToAddFromUser.Body;
            PostToAddToDB.Price = PostToAddFromUser.Price;
            PostToAddToDB.UploadDate = System.DateTime.Now;
            PostToAddToDB.NumberOfViews = 0;
            m_db.Post.Add(PostToAddToDB);
            int c = m_db .SaveChanges();
            return c > 0;
        }

        //הוספת צפיה למודעה לפי מזהה מודעה
        public ResponseDTO AddViewsToPost(int PostId)
        {
            Post postToAddViewo = GetPostByIdFromDB(PostId);
            if(postToAddViewo == null||postToAddViewo.Id!=PostId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המודעה הרצויה" };
            }
            postToAddViewo.NumberOfViews = postToAddViewo.NumberOfViews + 1;
            int c = m_db .SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = StatusCode.Success }
                :
                new ResponseDTO() { Status = StatusCode.Error, Message = "לא הצלחנו לעדכן אנא נסה שנית מאוחר יותר" };
        }

        //מחיקת מודעה למשתמש ומנהל
        public ResponseDTO DeletePost(int PostId)
        {
            User User = _UserService.GetUserByJWT();
            Post PostToDelete = GetPostByIdFromDB(PostId);
            List<Image> LImages = m_db.Image.Where(i => i.PostId == PostToDelete.Id).ToList();
            if(User.Role == "Classic" && PostToDelete.UserId != User.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message= "לא מורשה" };
            }
            if (PostToDelete == null || PostToDelete.Id != PostId)
            {
                return new ResponseDTO() { Status = StatusCode.Error, Message = "לא הצלחנו למצוא את המודעה המבוקשת" };
            }
            if (LImages.Count > 0)
            {
                for (int i = 0; i < LImages.Count; i++)
                {
                    string url = "https://localhost:44391/";
                    string UrlToDelete = LImages[i].Url.Substring(url.Length, LImages[i].Url.Length - url.Length);
                    var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
                    FileInfo file = new FileInfo(PathToDelete);
                    try
                    {
                        file.Delete();
                        m_db.SaveChanges();
                    }
                    catch
                    {
                        return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את התמונה" };
                    }
                }
            }
            m_db.Post.Remove(PostToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המתכון" };
        }

        //עדכון מודעה למי שייצר אותו
        public ResponseDTO UpdatePost(PostDTO PostToUpdateFromUser)
        {
            Post PostToUpdateToDb = GetPostByIdFromDB(PostToUpdateFromUser.Id);
            if(PostToUpdateToDb==null||PostToUpdateToDb.Id!=PostToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המודעה אנא נסה שנית מאוחר יותר" };
            }
            PostToUpdateToDb.SubcategoryId = PostToUpdateFromUser.SubcategoryId;
            PostToUpdateToDb.AreaId = PostToUpdateFromUser.AreaId;
            PostToUpdateToDb.Title = PostToUpdateFromUser.Title;
            PostToUpdateToDb.Body = PostToUpdateFromUser.Body;
            PostToUpdateToDb.Price = PostToUpdateFromUser.Price;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לעדכן את השינוים אנא נסה שנית מאוחר יותר" };
        }

        //JWT קבלת מזהה מודעה אחרון על פי
        public int GetLastPostIdByJWT()
        {
            return m_db.Post.Where(p => p.UserId == _UserService.GetUserIdByJWT()).ToList().Last().Id;
        }

        //קבלת מודעה לפי מזהה מודעה עם אויבקט
        public Post GetPostById(int postId)
        {
            var postobj = m_db.Post.Where(p => p.Id == postId).Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).FirstOrDefault();
            //postobj.Subcategory.Category = m_db.Category.Where(c => c.Id == postobj.Subcategory.CategoryId).FirstOrDefault();
            postobj.User.Password = null;
            return postobj;
        }


        //קבלת רשימת כל המודעות עם אוביקטים
        public List<Post>GetAllPost()
        {
            var postobj = m_db.Post.Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).ToList();
            for(int i=0;i<postobj.Count;i++)
            {
                //postobj[i].Subcategory.Category = m_db.Category.Where(c => c.Id == postobj[i].Subcategory.CategoryId).FirstOrDefault();
                postobj[i].User.Password = null;
            }        
            return postobj;
        }

        // JWT קבלת רשימת כל המודעות של משתמש למשתמש לפי 
        public List<Post>GetAllPostByJWTFoUser()
        {
            var postobj = m_db.Post.Where(p=>p.UserId==_UserService.GetUserIdByJWT()).Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).ToList();
            for (int i = 0; i < postobj.Count; i++)
            {
                //postobj[i].Subcategory.Category = m_db.Category.Where(c => c.Id == postobj[i].Subcategory.CategoryId).FirstOrDefault();
                postobj[i].User.Password = null;
            }
            return postobj;
        }

        //קבלת רשימת כל המודעות של משתמש מסוים לפי מזהה משתמש
        public List<Post> GetAllPostByUserId(int userId)
        {
            var postobj = m_db.Post.Where(p => p.UserId == userId).Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).ToList();
            for (int i = 0; i < postobj.Count; i++)
            {
                //postobj[i].Subcategory.Category = m_db.Category.Where(c => c.Id == postobj[i].Subcategory.CategoryId).FirstOrDefault();
                postobj[i].User.Password = null;
            }
            return postobj;
        }

        //קבלת רשימת כל המודעות לפי מזהה תת קטגוריה
        public List<Post> GetAllPostBySubcategoryId(int SubcategoryId)
        {
            var postobj = m_db.Post.Where(p => p.SubcategoryId == SubcategoryId).Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).ToList();
            for (int i = 0; i < postobj.Count; i++)
            {
                //postobj[i].Subcategory.Category = m_db.Category.Where(c => c.Id == postobj[i].Subcategory.CategoryId).FirstOrDefault();
                postobj[i].User.Password = null;
            }
            return postobj;
        }

        //קבלת רשימת כל המודעות לפי מזהה קטגוריה
        public List<Post>GetAllPostByCategoryId(int CategoryId)
        {
            var postobj = m_db.Post.Select(ee => new Post()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                AreaId = ee.AreaId,
                Title = ee.Title,
                Body = ee.Body,
                Price = ee.Price,
                UploadDate = ee.UploadDate,
                NumberOfViews = ee.NumberOfViews,
                User = ee.User,
                Subcategory = ee.Subcategory,
                Area = ee.Area
            }).ToList();
            List<Post>Lpost=new List<Post>();
            for (int i = 0; i < postobj.Count; i++)
            {
                //postobj[i].Subcategory.Category = m_db.Category.Where(c => c.Id == postobj[i].Subcategory.CategoryId).FirstOrDefault();
                postobj[i].User.Password = null;
                if(postobj[i].Subcategory.CategoryId == CategoryId)
                {
                    Lpost.Add(postobj[i]);
                }
            }
            return Lpost;
        }

        //קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר   
        public List<Post> GetAllPostByNumberOfViews()
        {
            List<Post> Lpost = GetAllPost();
            for(int i=0;i<Lpost.Count;i++)
            {
                for(int c=0;c<Lpost.Count;c++)
                {
                    if(Lpost[i].NumberOfViews>Lpost[c].NumberOfViews)
                    {
                        Post temp = Lpost[i];
                        Lpost[i] = Lpost[c];
                        Lpost[c] = temp;
                    }
                }
            }
            return Lpost;
        }

        //JWT קבלת רשימת המודעות בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        public List<Post> GetAllPostByNumberOfViewsByJWTForUser()
        {
            List<Post> Lpost = GetAllPostByJWTFoUser();
            for (int i = 0; i < Lpost.Count; i++)
            {
                for (int c = 0; c < Lpost.Count; c++)
                {
                    if (Lpost[i].NumberOfViews > Lpost[c].NumberOfViews)
                    {
                        Post temp = Lpost[i];
                        Lpost[i] = Lpost[c];
                        Lpost[c] = temp;
                    }
                }
            }
            return Lpost;
        }

        //קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר   
        public List<Post> GetAllPostByUploadDate()
        {
            List<Post> Lpost = GetAllPost();
            for (int i = 0; i < Lpost.Count; i++)
            {
                for (int c = 0; c < Lpost.Count; c++)
                {
                    if (Lpost[i].UploadDate > Lpost[c].UploadDate)
                    {
                        Post temp = Lpost[i];
                        Lpost[i] = Lpost[c];
                        Lpost[c] = temp;
                    }
                }
            }
            return Lpost;
        }

        //JWT קבלת רשימת המודעות בצורה ממוינת מי החדשים ביותר של משתמש על פי
        public List<Post> GetAllPostByUploadDateByJWTForUser()
        {
            List<Post> Lpost = GetAllPostByJWTFoUser();
            for (int i = 0; i < Lpost.Count; i++)
            {
                for (int c = 0; c < Lpost.Count; c++)
                {
                    if (Lpost[i].UploadDate > Lpost[c].UploadDate)
                    {
                        Post temp = Lpost[i];
                        Lpost[i] = Lpost[c];
                        Lpost[c] = temp;
                    }
                }
            }
            return Lpost;
        }

        //קבלת אחוזים כמה מודעות התוספו היום/ החודש/ השנה
        public int GetAllPrecentagePostUploadByDate(string RequestDate)
        {
            if (RequestDate == "Today")
            {
                return (100 / m_db.Post.Count()) * m_db.Post.Where(l => l.UploadDate == System.DateTime.Today).Count();
            }
            else if (RequestDate == "Month")
            {
                return (100 / m_db.Post.Count()) * m_db.Post.Where(l => l.UploadDate.Month == System.DateTime.Today.Month).Count();
            }
            return (100 / m_db.Post.Count()) * m_db.Post.Where(l => l.UploadDate.Year == System.DateTime.Today.Year).Count();
        }
    }
}
