using System;
using Yad2.Data.Entities;

namespace Yad2.Data.DTO
{
    public class FavoriteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Post Post { get; set; }
    }
}
