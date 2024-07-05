using ExamManagement.Business.DTOs.AuthDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface IAuthService
    {
        Task<TokenDTO> LoginAsync(LoginDTO loginDTO, string role);
    }
}
