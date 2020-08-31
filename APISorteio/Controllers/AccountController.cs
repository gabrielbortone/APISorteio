using APISorteio.DTOs;
using APISorteio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APISorteio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Administrador> _userManager;
        private readonly SignInManager<Administrador> _signInManager;

        public AccountController(
            UserManager<Administrador> userManager,
            SignInManager<Administrador> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdministradorDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(model);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(AdministradorDTO model)
        {
            var user = new Administrador { UserName = model.UserName,
                NormalizedUserName = model.UserName.ToUpper(), Email = model.Email, 
                NormalizedEmail = model.Email.ToUpper(), EmailConfirmed = true, 
                PhoneNumberConfirmed = true, TwoFactorEnabled = false,
                PhoneNumber= model.PhoneNumber };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
