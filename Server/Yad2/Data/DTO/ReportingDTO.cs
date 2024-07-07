namespace Yad2.Data.DTO
{
    public class ReportingDTO
    {
        public int Id { get; set; }
        public string Cause { get; set; }
        public int PostId { get; set; }
        public bool IsActive { get; set; }
        public string ClosingExplanation { get; set; }
    }
}
