using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class FavoriteService
    {
        //בנאי
        //קבלת מועדף נקי מאוביקטים לשימוש פנימי
        //הוספת מועדף
        //מחיקת מועדף
        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        //JWT קבלת רשימת מועדפים לפי 


        private readonly Yad2DbContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public FavoriteService(Yad2DbContext db, UserService userservice)
        {
            m_db = db;
            _UserService = userservice;
        }

        //קבלת מועדף נקי מאוביקטים לשימוש פנימי
        public Favorite GetFavoriteByIdFromDb(int FavoriteId)
        {
            return m_db.Favorite.Where(f=>f.Id == FavoriteId).FirstOrDefault();
        }

        //הוספת מועדף
        public bool AddFavorite(FavoriteDTO FavoriteToAddromUser)
        {
            Favorite FavoriteToAddToDb = new Favorite();
            FavoriteToAddToDb.UserId = _UserService.GetUserIdByJWT();
            FavoriteToAddToDb.PostId = FavoriteToAddromUser.PostId;
            FavoriteToAddToDb.DateAdded =System.DateTime.Now;
            m_db.Favorite.Add(FavoriteToAddToDb);
            int c =m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת מועדף
        public ResponseDTO DeleteFavorite(int FavoriteId)
        {
            Favorite FavoriteToDelete=GetFavoriteByIdFromDb(FavoriteId);
            if (FavoriteToDelete == null || FavoriteToDelete.Id != FavoriteId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המועדף" };
            }
            m_db.Favorite.Remove(FavoriteToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = StatusCode.Success }
                :
                new ResponseDTO() { Status=StatusCode.Error,Message="לא הצלחנו למחוק את המועדך"};
        }

        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        public Favorite GetFavoriteByFavoriteIdAndJWT(int FavoriteID)
        {
            var favorite = m_db.Favorite.Where(f => f.Id == FavoriteID && f.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Favorite()
            { 
                Id=ee.Id,
                UserId = ee.UserId,
                PostId=ee.PostId,
                Post=ee.Post

            }).FirstOrDefault();
            favorite.Post.User=m_db.User.Where(u=>u.Id==favorite.Post.UserId).FirstOrDefault();
            favorite.Post.Subcategory = m_db.Subcategory.Where(s => s.Id == favorite.Post.SubcategoryId).FirstOrDefault();
            favorite.Post.Subcategory.Category = m_db.Category.Where(c => c.Id == favorite.Post.Subcategory.CategoryId).FirstOrDefault();
            favorite.Post.Area = m_db.Area.Where(a => a.Id == favorite.Post.AreaId).FirstOrDefault();
            favorite.Post.User.Password = null;
            favorite.Post.User.Mail = null;
            return favorite;
        }

        //JWT קבלת רשימת מועדפים לפי 
        public List<Favorite> GetAllFavoriteByJWT()
        {
            var favorite = m_db.Favorite.Where(f => f.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Favorite()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                PostId = ee.PostId,
                Post = ee.Post

            }).ToList();
            return favorite;
        }


    }
}
