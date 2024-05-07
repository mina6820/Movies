using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.Authentication;
using Movies.DTOs;
using Movies.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration config;

        public AccountController
            (UserManager<AppUser> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GeneralResponse>> Register(UserRegisterDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var existingUserEmail = await userManager.FindByEmailAsync(userDto.Email);
                if (existingUserEmail != null)
                {
                    return new GeneralResponse { IsSuccess = false, Data = "Email already exists" };
                }

                var existingUserName = await userManager.FindByNameAsync(userDto.YourFavirotePerson);
                if (existingUserName != null)
                {
                    return new GeneralResponse { IsSuccess = false, Data = "UserName already exists" };
                }

                AppUser appuser = new AppUser()
                {
                    FirstName = userDto.FirstName,
                    LastName  = userDto.LastName,
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    PasswordHash = userDto.Password,
                    YourFavirotePerson = userDto.YourFavirotePerson,
                };

                IdentityResult result =
                    await userManager.CreateAsync(appuser, userDto.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appuser, "User");
                    return new GeneralResponse { IsSuccess = true, Data= "Account Created" };

                }
                return new GeneralResponse { IsSuccess = false, Data = result.Errors }; ;

            }
            return new GeneralResponse { IsSuccess = false, Data = ModelState }; ;

        }
        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Login(UserLoginDTO userDto)
        {
            if (ModelState.IsValid)
            {
                AppUser? userFromDb =
                     await userManager.FindByNameAsync(userDto.UserName);
                if (userFromDb != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userFromDb, userDto.Password);
                    if (found)
                    {
                        List<Claim> myclaims = new List<Claim>();
                        myclaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));
                        myclaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id));
                        myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(userFromDb);
                        foreach (var role in roles)
                        {
                            myclaims.Add(new Claim(ClaimTypes.Role, role));
                        }


                        var SignKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);



                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIss"],
                            audience: config["JWT:ValidAud"],
                            expires: DateTime.Now.AddHours(1),
                            claims: myclaims,
                            signingCredentials: signingCredentials);
                        return new GeneralResponse()
                        {
                            IsSuccess = true,
                            Data = new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expired = mytoken.ValidTo,
                            }
                        };

                    }
                }
                return new GeneralResponse() { 
                    IsSuccess=false,
                    Data = Unauthorized("Invalid account")
            };
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = ModelState
            };
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse>> Logout()
        {
            await HttpContext.SignOutAsync();
            return new GeneralResponse { IsSuccess = true, Data = "Logged out successfully" };
        }

    }
}
