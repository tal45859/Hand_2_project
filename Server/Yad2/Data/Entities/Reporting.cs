using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("Reporting")]
    public class Reporting
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Cause")]
        public string Cause { get; set; }

        [Column("PostId")]
        public int PostId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
        
        [Column("ClosingExplanation")]
        public string ClosingExplanation { get; set; }
    }
}
