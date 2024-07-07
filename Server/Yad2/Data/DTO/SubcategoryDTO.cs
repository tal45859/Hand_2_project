using Yad2.Data.Entities;

namespace Yad2.Data.DTO
{
    public class SubcategoryDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public virtual Category Category { get; set; }
    }
}
