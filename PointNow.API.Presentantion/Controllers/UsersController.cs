using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PointNow.API.Domain.Interfaces.Repositories;
using PointNow.API.Identity.Dtos;
using PointNow.API.Identity.Entitites;
using PointNow.API.Presentation.Controllers.Base;
using System.Security.Claims;

namespace PointNow.API.Presentation.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUsersService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        #region Methods

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _userService.FindAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> InsertUser(User model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return BadRequest("Já existe um usuário cadastrado para esse e-mail, favor especificar um novo endereço de e-mail.");

            user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest("Ocorreu um erro ao cadastrar o usuário, tente novamente.");

            await _userManager.AddClaimAsync(user, new Claim("IsAdm", model.IsAdm.ToString()));

            return Ok(user.Id);
        }
        #endregion
    }
}
