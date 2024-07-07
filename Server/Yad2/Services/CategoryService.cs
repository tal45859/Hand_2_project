using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class CategoryService
    {

        // בנאי
        // קבלת קטגוריה לפי מזהה
        // קבלת קטגוריה לפי שם קטגוריה
        // קבלת כל הקטגוריות
        // יצירת קטגוריה
        // מחיקת קטגוריה
        // עדכון קטגוריה




        //לעשות קטגרוריה בשם כלי
        //*************************************************************************************************************************
        //  לעשות שבמקרה שנמחקת קטגוריה לעבור על כל המודעות ולשנות את הערך לתת קטגרויה ברירת מחדל בשם כללי ולשנות לקטגוריה כללית*
        //*************************************************************************************************************************

        private readonly Yad2DbContext m_db;

        // בנאי
        public CategoryService(Yad2DbContext db)
        {
            m_db = db;
        }

        // קבלת קטגוריה לפי מזהה
        public Category GetCategoryById(int CategoryId)
        {
            return m_db.Category.Where(c=>c.Id==CategoryId).FirstOrDefault();
        }

        // קבלת קטגוריה לפי שם קטגוריה
        public Category GetCategoryByName(string CategoryName)
        {
            return m_db.Category.Where(c => c.CategoryName == CategoryName).FirstOrDefault();
        }

        // קבלת כל הקטגוריות
        public List<Category>GetAllCategory()
        {
            return m_db.Category.ToList();
        }

        // יצירת קטגוריה
        public bool AddCategory(CategoryDTO CategoryToADDFromUser)
        {
            Category CategoryToAddToDb =new Category();
            CategoryToAddToDb.CategoryName= CategoryToADDFromUser.CategoryName;
            m_db.Category.Add(CategoryToAddToDb);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        // מחיקת קטגוריה
        public ResponseDTO DeleteCategory(int CategoryId)
        {
            Category categoryToDeleteToDB = GetCategoryById(CategoryId);
            if(categoryToDeleteToDB==null|| categoryToDeleteToDB.Id!=CategoryId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את הקטגוריה אנא נסה שנית מאוחרי ותר " };
            }
            UpdatePostSubCCategoryIdByCategoryId(categoryToDeleteToDB.Id);
            m_db.Category.Remove(categoryToDeleteToDB);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את הקטגוריה אנא נסה שנית מאוחר יותר" };
        }

        // עדכון קטגוריה
        public ResponseDTO UpdateCategory(CategoryDTO CategoryToUpdateFromUser)
        {
            Category categoryToUpdateToDB = GetCategoryById(CategoryToUpdateFromUser.Id);
            if(categoryToUpdateToDB==null||CategoryToUpdateFromUser.Id!=categoryToUpdateToDB.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את הקטגוריה אנא נסה שנית מאוחר יותר" };
            }
            categoryToUpdateToDB.CategoryName = CategoryToUpdateFromUser.CategoryName;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את הקטגוריה אנא נסה שנית מאוחר יותר" };
        }


        //לבדוק לפני הרצה היטב*************************************************************
        //תת קטגוריה כללית זה 1 מצביעה על קטגכוריה כללית 1
        //לעדכן את כל המודעות שיצביעו מעכשיו על תת קטגוריה נכונה בעת מחיקת קטגוריה
        public void UpdatePostSubCCategoryIdByCategoryId(int categoryId)
        {
            List<Subcategory> LSubcategories = m_db.Subcategory.Where(s => s.CategoryId == categoryId).ToList();
            List<Post> LPost = new List<Post>();
            for (int i = 0; i < LSubcategories.Count; i++)
            {
                LPost = m_db.Post.Where(p => p.SubcategoryId == LSubcategories[i].Id).ToList();
                LPost.ForEach(p => p.SubcategoryId = 1);
                m_db.SaveChanges();
            }
        }

    }
}
