namespace Yad2.Data.DTO
{
    public enum StatusCode
    {
        Success = 200,
        Error,
        Warning 
    }
    public class ResponseDTO
    {
        public StatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
