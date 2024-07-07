using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class SubcategoryService
    {
        //תקציר
        ////////////
        //בנאי
        //קבלת תת קטגוריה לפי מזהה נקי מאוביקטים לשימוש פנימי
        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        // הוספת תת קטגוריה
        // עדכון תת קטגוריה
        // מחיקת תת קטגוריה
        // לעשות שבמקרה שנמחקת תת קטגוריה לעבור על כל המודעות ולשנות את הערך לתת קטגרויה ברירת מחדל בשם "כללי" בשביל שלא יהיו בעיות
        //לעדכן את כל המודעות שיצביעו מעכשיו על תת קטגוריה נכונה בעת מחיקת קטגוריה
        //******************************************************
        //שבכל קטגוריה יהיה תת קטגוריה שיהיה כללי לתת קטגוריה
        //********************************************************


        private readonly Yad2DbContext m_db;
        //בנאי
        public SubcategoryService(Yad2DbContext db)
        {
            m_db = db;
        }

        //קבלת תת קטגוריה לפי מזהה נקי מאוביקטים לשימוש פנימי
        public Subcategory GetSubcategoryFromDB(int SubcategoryId)
        {
            return m_db.Subcategory.Where(s => s.Id == SubcategoryId).FirstOrDefault();
        }

        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        public Subcategory GetSubcategoryById(int SubcategoryId)
        {
            var SubcategoryObj = m_db.Subcategory.Where(subcategory => subcategory.Id == SubcategoryId).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).FirstOrDefault();
            return SubcategoryObj;
        }

        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        public List<Subcategory> GetAllSubcategory()
        {
            var SubcategoryObj = m_db.Subcategory.Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).ToList();
            return SubcategoryObj;
        }


        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        public Subcategory GetSubcategoryByCategoryId(int CategoryId)
        {
            var SubCategoryObj = m_db.Subcategory.Where(s => s.CategoryId == CategoryId).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).FirstOrDefault();
            return SubCategoryObj;
        }

        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        public List<Subcategory> GetAllSubcategoryByCategoryId(int CategoryId)
        {
            var SubCategoryObj = m_db.Subcategory.Where(s => s.CategoryId == CategoryId).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).ToList();
            return SubCategoryObj;
        }

        // הוספת תת קטגוריה
        public bool AddSubCategory(SubcategoryDTO SubCategoryToAddFromUser)
        {
            Subcategory subcategoryToAddToDb = new Subcategory();
            subcategoryToAddToDb.SubcategoryName=SubCategoryToAddFromUser.SubcategoryName;
            subcategoryToAddToDb.CategoryId=SubCategoryToAddFromUser.CategoryId;
            m_db.Subcategory.Add(subcategoryToAddToDb);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        // עדכון תת קטגוריה
        public ResponseDTO UpdateSubcategory(SubcategoryDTO SubcategoryToUpdate)
        {
            Subcategory subcategoryToUpdateToDb = GetSubcategoryFromDB(SubcategoryToUpdate.Id);
            if(subcategoryToUpdateToDb == null||subcategoryToUpdateToDb.Id!=SubcategoryToUpdate.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא מצאנו את התת קטגוריה הרצויה אנא נסה שנית מאוחר יותר" };
            }
            subcategoryToUpdateToDb.SubcategoryName = SubcategoryToUpdate.SubcategoryName;
            subcategoryToUpdateToDb.CategoryId = SubcategoryToUpdate.CategoryId;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לעדכן את התת קטגוריה אנא נסה שנית מאוחר יותר" };
        }

        // מחיקת תת קטגוריה
        public ResponseDTO DeleteSubcategory(int SubcategoryId)
        {
            Subcategory subcategoryToDelete = GetSubcategoryFromDB(SubcategoryId);
            if(subcategoryToDelete==null||subcategoryToDelete.Id!= SubcategoryId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את התת קטגוריה אנא נסה שנית מאוחר יותר" };
            }
            if(UpdatePostToDeleteSubCategory(subcategoryToDelete.Id).Status==Data.DTO.StatusCode.Error)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לעדכן את ההת קטגוריה במודעה אנא נסה שנית מאוחר יותר" };
            }
            m_db.Subcategory.Remove(subcategoryToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את התת קטגוריה אנא נסה שנית מאוחר יותר" }; 
        }

        // לעשות שבמקרה שנמחקת תת קטגוריה לעבור על כל המודעות ולשנות את הערך לתת קטגרויה ברירת מחדל בשם "כללי" בשביל שלא יהיו בעיות
        public ResponseDTO UpdatePostToDeleteSubCategory(int SubcategoryId)
        {
            List<Post> Lpost = m_db.Post.Where(p => p.SubcategoryId == SubcategoryId).ToList();
            if(Lpost.Count==0)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
            }
            //תת קטגוריה כללית זה 1
            Lpost.ForEach(p => p.SubcategoryId = 1);
            int c= m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error };
        }
    }
}
