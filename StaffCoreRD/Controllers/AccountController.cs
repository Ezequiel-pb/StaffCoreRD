using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffCoreRD.Models;

namespace StaffCoreRD.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]//hola
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Verificamos si es el primer usuario registrado en la base de datos
                    if (_userManager.Users.Count() == 1)
                    {
                        // El primer usuario SIEMPRE será Administrador
                        await _userManager.AddToRoleAsync(user, "Administrador");
                    }
                    else
                    {
                        // Los usuarios siguientes serán Viewer por defecto
                        await _userManager.AddToRoleAsync(user, "Viewer");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Staff");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "La cuenta ha sido bloqueada debido a múltiples intentos fallidos. Intente más tarde.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Credenciales incorrectas. Verifique su correo y contraseña.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: Account/ManageRoles
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ManageRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesList = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesList.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? string.Empty,
                    RoleActual = roles.FirstOrDefault() ?? "Sin Rol"
                });
            }

            return View(userRolesList);
        }

        // POST: Account/ChangeRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);

                TempData["Exito"] = $"Rol de {user.Email} actualizado a '{newRole}' correctamente.";
            }
            return RedirectToAction(nameof(ManageRoles));
        }
    }
}