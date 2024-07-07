using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("PostId")]
        public int PostId { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }

    }
}
