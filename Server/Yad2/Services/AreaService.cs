using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class AreaService
    {
        //בנאי
        //קבלת רשימת כל הערים
        //קבלת עיר לפי מזהה
        //קבלת רשימת ערים לפי מזהה אזור
        //הוספת עיר מוגבל למנהל
        //מחיקת עיר מוגבל למנהל
        //עדכון עיר מוגבל למנהל
        //עדכון בכל המודעות בעת מחיקת אזור שיש במודעה לאזור בשם "כל הארץ"ידני

        private readonly Yad2DbContext m_db;

        //בנאי
        public AreaService(Yad2DbContext db)
        {
            m_db = db;
        }

        //קבלת רשימת כל הערים
        public List<Area>GetAllArea()
        {
            return m_db.Area.ToList();
        }

        //קבלת עיר לפי מזהה
        public Area GetAreaById(int AreaId)
        {
            return m_db.Area.Where(a=>a.Id == AreaId).FirstOrDefault();
        }

        //קבלת רשימת ערים לפי מזהה אזור
        public List<Area> GetByTopAreaId(int TopAreaId)
        {
            return m_db.Area.Where(a=>a.TopAreaId == TopAreaId).ToList();
        }

        //הוספת עיר מוגבל למנהל
        public bool AddArea(AreaDTO AreaToAddFromUser)
        {
            Area AreaToAddToDB= new Area();
            AreaToAddToDB.AreaName = AreaToAddFromUser.AreaName;
            AreaToAddToDB.TopAreaId = AreaToAddFromUser.TopAreaId;
            m_db.Area.Add(AreaToAddToDB);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת עיר מוגבל למנהל
        public ResponseDTO DeleteArea(int AreaId)
        {
            Area AreaToDelete = GetAreaById(AreaId);
            if (AreaToDelete == null|| AreaId != AreaToDelete.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את האזור" };
            }
            if (DeleteAreaInThePost(AreaToDelete.Id).Status == StatusCode.Error)
            {
                // משנה את הערך בכל המודעות לערך כלל הארץ שהוא כללי ברגע שמוחקים את העיר
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לעדכן את המודעות בעל מזהה שטח זה" };
            }
            m_db.Area.Remove(AreaToDelete);
            int c=m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את האזור אנא נסה שנית מאוחר יותר" };
        }

        //עדכון עיר מוגבל למנהל
        public ResponseDTO UpdateArea(AreaDTO AreaToUpdateFromUser)
        {
            Area AreToUpdateToDB = GetAreaById(AreaToUpdateFromUser.Id);
            if (AreToUpdateToDB == null || AreToUpdateToDB.Id!= AreaToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את האזור" };
            }
            AreToUpdateToDB.AreaName = AreaToUpdateFromUser.AreaName;
            AreToUpdateToDB.TopAreaId = AreaToUpdateFromUser.TopAreaId;
            int c=m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו לעדכן את האזור אנא נסה שנית מאוחר יותר" };
        }

        //עדכון בכל המודעות בעת מחיקת אזור שיש במודעה לאזור בשם "כל הארץ"ידני
        public ResponseDTO DeleteAreaInThePost(int AreaId)
        {
            List<Post>LPost=m_db.Post.Where(p=>p.AreaId==AreaId).ToList();
            if(LPost.Count==0)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
            }
            int NewAreaId = m_db.Area.Where(a => a.AreaName == "כל הארץ").FirstOrDefault().Id;
            LPost.ForEach(p => p.AreaId = NewAreaId);
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status=Data.DTO.StatusCode.Success}
                :
                new ResponseDTO() { Status=Data.DTO.StatusCode.Error};
        }


    }
}
