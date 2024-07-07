using System;
using Yad2.Data.Entities;

namespace Yad2.Data.DTO
{
    public class LoginHistoryDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual User User { get; set; }
    }
}
