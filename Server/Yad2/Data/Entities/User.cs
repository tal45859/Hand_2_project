using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad2.Data.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Mail"),MaxLength(150)]
        public string Mail { get; set; }

        [Column("FirstName"), MaxLength(50)]
        public string FirstName { get; set; }

        [Column("LastName"), MaxLength(50)]
        public string LastName { get; set; }

        [Column("Password"), MaxLength(50)]
        public string Password { get; set; }

        [Column("Role"), MaxLength(50)]
        public string Role { get; set; }

        [Column(TypeName ="date")]
        public DateTime RegisterDate { get; set; }

        [Column(TypeName ="date")]
        public DateTime Birthdate { get; set; }

        [Column("Phone"), MaxLength(50)]
        public string Phone { get; set; }
        
        [Required]
        public virtual ICollection<Post> Post { get; set; }

        [Required]
        public virtual ICollection<LoginHistory> LoginHistory { get; set; }



    }
}
