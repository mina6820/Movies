using Microsoft.AspNetCore.Http;
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
                    return new GeneralResponse { IsSuccess = true, Data= "Account Created" };;
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
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
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expired = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized("Invalid account");
            }
            return BadRequest(ModelState);
        }

    }
}
