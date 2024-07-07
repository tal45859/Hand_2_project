using System;
using Yad2.Data.Entities;

namespace Yad2.Data.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubcategoryId { get; set; }
        public int AreaId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Price { get; set; }
        public DateTime UploadDate { get; set; }
        public int NumberOfViews { get; set; }
        public virtual User User { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual Area Area { get; set; }
    }
}
