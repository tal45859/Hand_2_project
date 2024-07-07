using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class UserService
    {
        //בנאי
        // הזדאות
        //קבלת תוקןJWT
        // JWT קבלת אוביקט משתמש על פי
        // JWT קבלת מזהה משתמש על פי
        // הוספת משתמש
        // קבלת משתמש לפי מזהה
        // קבלת משתמש לשימוש פנימי
        // קבלת כל המשתמשים
        // קבלת כל המנהלים מוגבל למנהל
        // קבלת משתמש על פי מייל
        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        // JWT עדכון משתמש על פי
        // JWT מחיקת משתמש על פי 
        // מחיקת משתמש למנהל 
        // מחיקת כל המועדפים של משתמש בעת מחיקת משתמש 
        // עדכון תפקיד למשתמש מוגבל למנהל 
        // הצפנת סיסמה
        // האם קיים מייל כזה
        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        //יצירת סיסמה חדשה מוצפנת
        //קבלת כמה אחוז משתמשים יוזרים קלאסים


        private readonly Yad2DbContext m_db;
        private readonly JwtService _jwtService;

        //בנאי
        public UserService(Yad2DbContext db,JwtService jwt)
        {
            m_db = db;
            _jwtService = jwt;
        }


        // הזדאות
        public User GetUserForLogin(AuthRequestDTO Request)
        {
            string passwordAfterMD5 = GetMD5(Request.Password);
            return m_db.User.Where(u => u.Mail.ToLower() == Request.Email.ToLower() && u.Password == passwordAfterMD5).FirstOrDefault();
        }

        //קבלת תוקןJWT
        public string GetToken(string id ,string role)
        {
            return _jwtService.GenerateToken(id, role);
        }

        // JWT קבלת אוביקט משתמש על פי
        public User GetUserByJWT()
        {
            return m_db.User.Where(u => u.Id == int.Parse(_jwtService.GetTokenClaims())).FirstOrDefault();
        }

        // JWT קבלת מזהה משתמש על פי
        public int GetUserIdByJWT()
        {
            return int.Parse(_jwtService.GetTokenClaims());
        }

        // הוספת משתמש
        public bool AddUser(UserDTO UserFromUserToAdd)
        {
            if(GetHaveMail(UserFromUserToAdd.Mail))
            {
                return false;
            }
            User UserToAddToDB = new User();
            UserToAddToDB.FirstName = UserFromUserToAdd.FirstName;
            UserToAddToDB.LastName = UserFromUserToAdd.LastName;
            UserToAddToDB.Mail = UserFromUserToAdd.Mail;
            UserToAddToDB.Phone = UserFromUserToAdd.Phone;
            UserToAddToDB.Password = GetMD5(UserFromUserToAdd.Password);
            UserToAddToDB.RegisterDate = DateTime.Now;
            UserToAddToDB.Birthdate = UserFromUserToAdd.Birthdate;
            UserToAddToDB.Role = "Classic";
            m_db.User.Add(UserToAddToDB);
            int c =m_db.SaveChanges();
            return c > 0;
        }

        // קבלת משתמש לפי מזהה
        public User GetUserById(int Id)
        {
            User user = m_db.User.Where(u=>u.Id == Id).FirstOrDefault();
            user.Mail = null;       
            user.Phone = null;       
            user.Password = null;   
            return user;
        }

        // קבלת משתמש לשימוש פנימי
        public User GetUserByIdFromDb(int Id)
        {
            return m_db.User.Where(u => u.Id == Id).FirstOrDefault();
        }

        // קבלת כל המשתמשים
        public List<User>GetAllUser()
        {
            List<User> LUser = m_db.User.ToList();
            LUser.ForEach(u => u.Password = null);
            return LUser;
        }

        // קבלת כל המנהלים מוגבל למנהל
        public List<User>GetAllAdmin()
        {
            List<User> LUser = m_db.User.Where(u => u.Role == "Admin").ToList();
            LUser.ForEach(u=>u.Password = null);
            return LUser;
        }

        // קבלת משתמש על פי מייל
        public User GetUserByMail(string mail)
        {
            User user = m_db.User.Where(u=>u.Mail == mail).FirstOrDefault();
            user.Password = null;
            return user;
        }

        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        public List<User>GetAllUserNotAdmin()
        {
            List<User>LUser=m_db.User.Where(u=>u.Role != "Admin").ToList();
            LUser.ForEach(u => u.Password = null);
            return LUser;
        }

        // JWT עדכון משתמש על פי
        public ResponseDTO UpdateUserFromJWT(UserDTO UserToUpdateFromUser)
        {
            User userFromDBToUpdate = GetUserByJWT();
            if(userFromDBToUpdate==null||userFromDBToUpdate.Id!= UserToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המשתמש אנא נסה שנית מאוחר יותר" };
            }
            userFromDBToUpdate.FirstName = UserToUpdateFromUser.FirstName;
            userFromDBToUpdate.LastName = UserToUpdateFromUser.LastName;
            userFromDBToUpdate.Phone = UserToUpdateFromUser.Phone;
            userFromDBToUpdate.Birthdate = UserToUpdateFromUser.Birthdate;
            if(userFromDBToUpdate.Password!= UserToUpdateFromUser.Password)
            {
                userFromDBToUpdate.Password = GetMD5(UserToUpdateFromUser.Password);
            }
            int c= m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success } 
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לשמור את השינוים אנה נסה שנית מאוחר יותר" };
        }

        // JWT מחיקת משתמש על פי 
        public ResponseDTO DeleteUserForUserByJWT()
        {
            User UserToDelete = GetUserByJWT();
            if (UserToDelete == null)
            {
                return new ResponseDTO() { Status=Data.DTO.StatusCode.Error,Message="לא הצלחנו למצוא את המשתמש"};   
            }
            if(DeleteAllFavorite(UserToDelete.Id).Status==StatusCode.Error)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המועדפים" };
            }
             m_db.User.Remove(UserToDelete);
            int c= m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המשתמש אנה נסה שנית מאוחר יותר" };
        }

        // מחיקת משתמש למנהל 
        public ResponseDTO DeleteUserForAdmin(int UserId)
        {
            User UserToDelete = GetUserByIdFromDb(UserId);
            if(UserToDelete == null|| UserToDelete.Id!=UserId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המשתמש אנא נסה שנית מאוחר יותר" };
            }
            if (DeleteAllFavorite(UserToDelete.Id).Status == StatusCode.Error)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המועדפים" };
            }
            m_db.User.Remove(UserToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המשתמש אנה נסה שנית מאוחר יותר" };
        }

        // מחיקת כל המועדפים של משתמש בעת מחיקת משתמש 
        public ResponseDTO DeleteAllFavorite(int UserId)
        {
           List<Favorite> LFavorite= m_db.Favorite.Where(f=>f.UserId==UserId).ToList();
            if(LFavorite.Count==0)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error };
            }
            LFavorite.ForEach(f => m_db.Favorite.Remove(f));
            int c= m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את המועדפים אנה נסה שנית מאוחר יותר" };
        }

        // עדכון תפקיד למשתמש מוגבל למנהל 
        public ResponseDTO UpdateRoleForAdmin(ChangeUserRoleDTO ChangeUserRole)
        {
            User UserToUpdateRole = GetUserByIdFromDb(ChangeUserRole.UserId);
            if(UserToUpdateRole == null||UserToUpdateRole.Id!= ChangeUserRole.UserId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את המשתמש אנא נה שנית מאוחר יותר" };
            }
            UserToUpdateRole.Role = ChangeUserRole.Role;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status= Data.DTO.StatusCode.Success}
                :
                new ResponseDTO() { Status=Data.DTO.StatusCode.Error,Message="לא הצלחנו לעדכן את התפקיד למשתמש"};
        }

        // הצפנת סיסמה
        private string GetMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        // האם קיים מייל כזה
        public bool GetHaveMail(string mail)
        {
            return m_db.User.Where(u => u.Mail == mail).Count() > 0;
        }

        public bool ForgotPassword(string mailto)
        {
            User user = GetUserByMail(mailto);
            string newPassword = NewPassword();
            if (user == null || user.Mail != mailto)
            {
                return false;
            }
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("fromthefarmerisrael@gmail.com");
                message.To.Add(new MailAddress(mailto));
                message.Subject = "בקשתך לשינוי סיסמה";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<table><tr><th> Yad2 Israel </th></tr><tr><td> שלום רב סיסמתך החדשה היא:</td></tr><tr><td>" + newPassword + "</td></tr> <tr><td> חברת Cook Book Israel </td></tr></table>";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("fromthefarmerisrael@gmail.com", "Tt123456@");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            user.Password = GetMD5(newPassword);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //יצירת סיסמה חדשה מוצפנת
        private string NewPassword()
        {
            string password = "";
            char[] chars = "$%#@!?;:+-^&*abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random r = new Random();
            int LenghtPassword = r.Next(8, 13);//8-12//בגלל ש4 אני שם בכוח
            password += chars[r.Next(0, 13)];//0-12
            password += chars[r.Next(13, 39)];//13-38
            for (int i = 0; i < LenghtPassword; i++)
            {
                int RandomNumber = r.Next(0, 4);
                if (RandomNumber == 0)
                {
                    password += chars[r.Next(0, 13)];//0-12
                }
                else if (RandomNumber == 1)
                {
                    password += chars[r.Next(13, 39)];//13-38
                }
                else if (RandomNumber == 2)
                {
                    password += chars[r.Next(39, 49)];//38-48
                }
                else
                {
                    password += chars[r.Next(49, chars.Length)];//48-chars.Lenght
                }
            }
            password += chars[r.Next(39, 49)];//38-48
            password += chars[r.Next(49, chars.Length)];//48-chars.Lenght      
            return password;
        }

        //קבלת כמה אחוז משתמשים יוזרים קלאסים
        public int GetAllPrecentageClassicUser()
        {
            return (100/ m_db.User.Count())*m_db.User.Where(user=>user.Role== "Classic").Count();
        }
    }
}
