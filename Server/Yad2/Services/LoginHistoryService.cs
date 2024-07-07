using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class LoginHistoryService
    {
        //בנאי
        //קבלת אוביקט לפי מזהה נקי מאוביקטים לשימוש פנימי
        //הוספת היסטוריה
        //מחיקת היסטוריה לפי מזהה היסטוריה
        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //קבלת אחוזים כמה התחברו היום/ החודש/ השנה

        private readonly Yad2DbContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public LoginHistoryService(Yad2DbContext db,UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת אוביקט לפי מזהה נקי מאוביקטים לשימוש פנימי
        public LoginHistory GetLoginHistoryByIdFromDb(int Id)
        {
            return m_db.LoginHistory.Where(l=>l.Id == Id).FirstOrDefault();
        }

        //הוספת היסטוריה
        public bool AddLoginHistory()
        {
            LoginHistory loginHistory = new LoginHistory();
            loginHistory.UserId = _UserService.GetUserIdByJWT();
            loginHistory.DateAdded = System.DateTime.Now;
            m_db.LoginHistory.Add(loginHistory);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת היסטוריה לפי מזהה היסטוריה
        public ResponseDTO DeleteLoginHisstory(int Id)
        {
            LoginHistory loginHistoryToDelete = GetLoginHistoryByIdFromDb(Id);
            if(loginHistoryToDelete == null||loginHistoryToDelete.Id!= Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את ההיסטוריה" };
            }
            m_db.LoginHistory.Remove(loginHistoryToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את ההיסטוריה אנא נסה שנית מאוחר יותר " };
        }

        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        public LoginHistory GetLoginHistoryById(int Id)
        {
            var loginObj = m_db.LoginHistory.Where(l => l.Id == Id).Select(ee => new LoginHistory()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).FirstOrDefault();
            loginObj.User.Password = null;
            return loginObj;
        }

        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        public List<LoginHistory> GetAllLoginHistory()
        {
            var loginObj = m_db.LoginHistory.Select(ee => new LoginHistory()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).ToList();
            loginObj.ForEach(l => l.User.Password = null);
            return loginObj;
        }

        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        public List<LoginHistory> GetAllLoginHistoryByUserId(int userId)
        {
            var loginObj = m_db.LoginHistory.Where(l=>l.UserId==userId).Select(ee => new LoginHistory()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).ToList();
            loginObj.ForEach(l => l.User.Password = null);
            return loginObj;
        }


        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //Today=היום   Month=החודש   AllTheTime=כל הזמנים   Year= השנה
        public List<LoginHistory> GetLoginHistoryFilteringByDate(string RequestDate)
        {
            if( RequestDate == "Today")
            {
                var loginObj = m_db.LoginHistory.Where(l => l.DateAdded == System.DateTime.Today).Select(ee => new LoginHistory()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    DateAdded = ee.DateAdded,
                    User = ee.User
                }).ToList();
                loginObj.ForEach(l => l.User.Password = null);
                return loginObj;
            }
            else if( RequestDate == "Month")
            {
                var loginObj = m_db.LoginHistory.Where(l => l.DateAdded.Month == System.DateTime.Today.Month).Select(ee => new LoginHistory()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    DateAdded = ee.DateAdded,
                    User = ee.User
                }).ToList();
                loginObj.ForEach(l => l.User.Password = null);
                return loginObj;
            }
            else if( RequestDate == "Year")
            {
                var loginObj = m_db.LoginHistory.Where(l => l.DateAdded.Year == System.DateTime.Today.Year).Select(ee => new LoginHistory()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    DateAdded = ee.DateAdded,
                    User = ee.User
                }).ToList();
                loginObj.ForEach(l => l.User.Password = null);
                return loginObj;
            }
            return GetAllLoginHistory();
        }

        //קבלת אחוזים כמה התחברו היום/ החודש/ השנה
        public int GetAllPrecentageLoginHistoryUserByDate(string RequestDate)
        {
            if (RequestDate == "Today")
            {
                return (100 / m_db.LoginHistory.Count()) * m_db.LoginHistory.Where(l => l.DateAdded == System.DateTime.Today).Count();
            }
            else if (RequestDate == "Month")
            {
                return (100 / m_db.LoginHistory.Count()) * m_db.LoginHistory.Where(l => l.DateAdded.Month == System.DateTime.Today.Month).Count();
            }
             return (100 / m_db.LoginHistory.Count()) * m_db.LoginHistory.Where(l => l.DateAdded.Year == System.DateTime.Today.Year).Count();       
        }
    }
}
