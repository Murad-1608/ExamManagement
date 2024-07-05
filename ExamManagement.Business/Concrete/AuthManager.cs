using Entity.Concrete.Identity;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.AuthDTOs;
using ExamManagement.Business.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ExamManagement.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public AuthManager(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                throw new UserNotFoundException("Email or password invalid");

            var role = await _userManager.GetRolesAsync(user);
            if (!role.Contains(roleName))
                throw new UserNotFoundException("Email or password invalid");


            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (result.Succeeded)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(100, roleName);
                return token;
            }

            throw new UserNotFoundException("Email or password invalid");
        }
    }
}
