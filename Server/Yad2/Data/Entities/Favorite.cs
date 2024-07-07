using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("Favorite")]
    public class Favorite
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("PostId"),Required]
        public int PostId { get; set; }

        [Column(TypeName ="date")]
        public DateTime DateAdded { get; set; }


        [Required]
        public virtual Post Post { get; set; }
    }
}
