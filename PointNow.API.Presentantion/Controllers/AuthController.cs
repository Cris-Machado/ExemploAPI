using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PointNow.API.Identity.Dtos;
using PointNow.API.Identity.Entitites;
using PointNow.API.Presentation.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PointNow.API.Presentation.Controllers
{
    public class AuthController : BaseController
    {
        #region ## Properties
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        #endregion

        #region ## Ctor
        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region ## Methods
        [HttpPost]
        [Route("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(Login model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (!result.Succeeded) return BadRequest("Usuário ou senha inválidos");

                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                if (appUser is null)
                    return BadRequest("Usuário não encontrado");

                var claims = await _userManager.GetClaimsAsync(appUser);
                var token = GenerateJwtToken(model.Email, appUser, claims).ToString();

                if (string.IsNullOrEmpty(token)) return BadRequest("Erro ao gerar o token.");

                return Ok(new UserToken
                {
                    AccessToken = token,
                    Name = appUser.Name,
                    Email = appUser.Email,
                    IsAdm = ClaimIsAdm(claims),
                    Expires = DateTime.Now.AddDays(Convert.ToDouble(1)),
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion

        #region ## Private Methods
        private object GenerateJwtToken(string email, IdentityUser user, IList<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SCHEDULE_XYZ_XPTO"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(1));

            var token = new JwtSecurityToken(
                "http://127.0.0.1",
                "http://127.0.0.1",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool ClaimIsAdm(IList<Claim> claims)
        {
            var isAdm = claims.FirstOrDefault(x => x.Type.Equals("IsAdm")).Value;
            return isAdm.Equals("True") ? true : false;
        }
        #endregion
    }
}
