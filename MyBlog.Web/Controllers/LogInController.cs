using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MyBlog.Entity.Concrete;
using MyBlog.Web.ViewModels;

namespace MyBlog.Web.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LogInController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel viewModel, string returnUrl = null)
        {


            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(viewModel.Email);

                if (user != null)
                {

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz");
                        return View(viewModel);
                    }

                    if(!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "E-posta adresiniz onaylanmamıştır. Lütfen E-postanı kontrol ediniz.");
                        return View(viewModel);
                    }


                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, true, true);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(String.Empty, "5 kez E-Posta adresinizi veya Şifrenizi yanlış girdiğiniz için hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz");
                        return View(viewModel);

                    }


                }
                else
                {
                    ModelState.AddModelError("", "E-Posta adresiniz veya Şifreniz yanlıştır.");

                    return View(viewModel);
                }

                ModelState.AddModelError("", $"E-Posta adresiniz veya Şifreniz yanlıştır. {await _userManager.GetAccessFailedCountAsync(user)}  kez başarısız işlem gerçekleşmiştir.");
            }

          

       

            return View(viewModel);
        }


         
    }
}

