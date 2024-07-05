using ExamManagement.Business.DTOs.AuthDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(int second, string role);
    }
}
