namespace ExamManagement.Business.DTOs.AuthDTOs
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string RefleshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
