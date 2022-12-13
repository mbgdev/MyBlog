using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Helper;
using MyBlog.Web.ViewModels;

namespace MyBlog.Web.Controllers
{
    public class PasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public PasswordController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {

            AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user != null)
            {
                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                string passwordResetLink = Url.Action("ResetPasswordConfirm", "Password", new
                {
                    userId = user.Id,
                    token = passwordResetToken,

                }, HttpContext.Request.Scheme);


               PasswordReset.PasswordResetSendEmail(passwordResetLink, viewModel.Email,user.UserName);

                ViewBag.status = "Şifre yenileme linki e-posta adresinize gönderilmiştir";




            }
            else
            {
                ModelState.AddModelError("", "Sistemde Kayıtlı E-Posta Adresi Bulunamadı");
            }

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult ResetPasswordConfirm(string userId, string token)
        {


            TempData["userId"] = userId;
            TempData["token"] = token;



            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(NewPasswordViewModel viewModel)
        {

            if (viewModel.UserId == null)
            {
                viewModel.UserId = TempData["userId"].ToString();
                viewModel.Token = TempData["token"].ToString();
            }



            if (ModelState.IsValid)
            {

                AppUser user = await _userManager.FindByIdAsync(viewModel.UserId);


                if (user != null)
                {
                                    
                    IdentityResult result = await _userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);

                        TempData["PasswordResetInfo"] = "Şifreniz başarı bir şekilde yenilenmiştir.";

                        await _signInManager.SignOutAsync();

                        return RedirectToAction("LogIn","LogIn");

                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(String.Empty, item.Description);
                        }
                    }
                }

                else
                {

                    ModelState.AddModelError("", "Hata meydana geldi. Tekrar deneyiniz");


                    return View(viewModel);
                }


            }


            return View(viewModel);
        }


       


    }
}
