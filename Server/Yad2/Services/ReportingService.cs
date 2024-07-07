using System.Collections.Generic;
using System.Linq;
using Yad2.Data;
using Yad2.Data.DTO;
using Yad2.Data.Entities;

namespace Yad2.Services
{
    public class ReportingService
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

        private readonly Yad2DbContext m_db;
        //בנאי
        public ReportingService(Yad2DbContext db)
        {
            m_db = db;
        }

        //קבלת דיווח לפי מזהה
        public Reporting GetReportingById(int ReportingId)
        {
            return m_db.Reporting.Where(r=>r.Id == ReportingId).FirstOrDefault();
        }

        //הוספת דיווח
        public bool AddReporting(ReportingDTO ReportingToAddFromUser)
        {
            Reporting ReportingToAddToDB = new Reporting();
            ReportingToAddToDB.Cause = ReportingToAddFromUser.Cause;
            ReportingToAddToDB.PostId = ReportingToAddFromUser.PostId;
            ReportingToAddToDB.IsActive = false;//לא פתור
            m_db.Reporting.Add(ReportingToAddToDB);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת דיווח
        public ResponseDTO DeleteReporting(int ReportingId)
        {
            Reporting ReportingToDelete = GetReportingById(ReportingId);
            if(ReportingToDelete == null|| ReportingToDelete.Id!= ReportingId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את הדיווח" };
            }
            m_db.Reporting.Remove(ReportingToDelete);
            int c= m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success}
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למחוק את הדיווח" };
        }

        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        public ResponseDTO UpdateReporting(ReportingDTO ReportingToUpdateFromUser)
        {
            Reporting ReportingToUpdateToDb = GetReportingById(ReportingToUpdateFromUser.Id);
            if(ReportingToUpdateToDb==null||ReportingToUpdateToDb.Id!= ReportingToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, Message = "לא הצלחנו למצוא את הדיווח" };
            }
            ReportingToUpdateToDb.ClosingExplanation = ReportingToUpdateFromUser.ClosingExplanation;
            ReportingToUpdateToDb.IsActive = ReportingToUpdateFromUser.IsActive;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status=Data.DTO.StatusCode.Success}
                :
                new ResponseDTO() {Status=Data.DTO.StatusCode.Error,Message="לא הצלחנו לעדכן את הדיווח" };
        }

        //קבלת רשימת דיווחים לפי מזהה מודעה
        public List<Reporting>GetAllReporingByPostId(int PostId)
        {
            return m_db.Reporting.Where(r=>r.PostId==PostId).ToList();
        }

        //קבלת רשימת כל הדיווחים
        public List<Reporting> GetAllReporting()
        {
            return m_db.Reporting.ToList();
        }

        //קבלת רשימת דיווחים לא פתורים
        public List<Reporting> GetAllReportingActive()
        {
            return m_db.Reporting.Where(r => r.IsActive == false).ToList();//false לא פתור
        }

        //קבלת רשימת דיווחים פתורים
        public List<Reporting> GetAllReportingNoActive()
        {
            return m_db.Reporting.Where(r => r.IsActive == true).ToList();//true  פתור
        }


    }
}
