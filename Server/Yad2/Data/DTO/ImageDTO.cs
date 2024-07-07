using System;

namespace Yad2.Data.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
