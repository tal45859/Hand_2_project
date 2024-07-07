using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("Area")]
    public class Area
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("AreaName"),MaxLength(50)]
        public string AreaName { get; set; }

        [Column("TopAreaId")]
        public int TopAreaId { get; set; }




        [Required]
        public virtual ICollection<Post> Post { get; set; }
    }
}
