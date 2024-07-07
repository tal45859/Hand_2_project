using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("Post")]
    public class Post
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId"),Required]
        public int UserId { get; set; }

        [Column("SubcategoryId"), Required]
        public int SubcategoryId { get; set; }

        [Column("AreaId"), Required]
        public int AreaId { get; set; }
        
        [Column("Title"),MaxLength(150)]
        public string Title { get; set; }

        [Column("Body"),MaxLength(150)]
        public string Body { get; set; }

        [Column("Price"),MaxLength(150)]
        public string Price { get; set; }

        [Column(TypeName ="date")]
        public DateTime UploadDate { get; set; }

        [Column("NumberOfViews")]
        public int NumberOfViews { get; set; }


        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Subcategory Subcategory { get; set; }

        [Required]
        public virtual Area Area { get; set; }

        [Required]
        public virtual ICollection<Favorite> Favorite { get; set; }
    }
}
