using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> _UserManager { get; }
        public SignInManager<ApplicationUser> _SignInManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FName = registerViewModel.FName,
                    Email = registerViewModel.Email,
                    LName = registerViewModel.LName,
                    IsAgree = registerViewModel.IsAgree,
                    UserName = registerViewModel.Email.Split('@')[0]
                };
                var result =await _UserManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                else
                {
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }

            }
            return View(registerViewModel);
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {

                var result = await _UserManager.FindByEmailAsync(logInViewModel.Email);
                if(result is not null)
                {
              bool flag= await  _UserManager.CheckPasswordAsync(result, logInViewModel.Password);
                    if (flag)
                    {
                   var status=   await  _SignInManager.PasswordSignInAsync(result, logInViewModel.Password, logInViewModel.RememberMe, false);
                        if (status.Succeeded)
                       return  RedirectToAction("Index", "Home");
                       

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wrong Password");
                    }
                }
            }
            return View(logInViewModel);
        }
        public new async Task<IActionResult> SignOut()
        {
           await _SignInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
     public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var User = await _UserManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var token = _UserManager.GeneratePasswordResetTokenAsync(User);
                    var RestPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email , Token = token },Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = RestPasswordLink
                    };
                    EmailSetting.SendMail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Not Valid");
                }
            }

            return View("ForgetPassword", model);




        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["Email"] as string;
                string token = TempData["Token"] as string;
                var user =await _UserManager.FindByEmailAsync(email);
            var result=  await  _UserManager.ResetPasswordAsync(user, token, model.NewPassword);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

    }
}
