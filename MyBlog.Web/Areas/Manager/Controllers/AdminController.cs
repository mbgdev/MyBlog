
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entity.Concrete;
using MyBlog.Web.Areas.Manager.ViewModels;
using System.Data;

namespace MyBlog.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Users()
        {

            var user = await _userManager.Users.ToListAsync();

            return View(_mapper.Map<List<AdminUserListViewModel>>(user));
        }

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await _roleManager.Roles.OrderBy(x=>x.Name).ToListAsync();
            return View(_mapper.Map<List<RoleViewModel>>(roles));
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleViewModel viewModel)
        {
            AppRole role = new();

            role.Name = viewModel.Name;

            IdentityResult result =await _roleManager.CreateAsync(role);



            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                }
            }



            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> RoleDelete(string id)
        {

            AppRole role = await _roleManager.FindByIdAsync(id);

            
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

            }

            return RedirectToAction("Roles");
        }



        [HttpGet]
        public async Task<IActionResult> RoleUpdate(string id)
        {
           

            AppRole role =await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(_mapper.Map<RoleViewModel>(role));

            }

            return RedirectToAction("Roles");
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleViewModel viewModel)
        {
            AppRole role = await _roleManager.FindByIdAsync(viewModel.Id);

            if (role != null)
            {
                role.Name = viewModel.Name;


                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {

                    return RedirectToAction("Roles");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Güncelleme Başarısız Oldu");
            }

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;

            AppUser user = _userManager.FindByIdAsync(id).Result;

            ViewBag.userName = user.UserName;

            IQueryable<AppRole> roles = _roleManager.Roles;

            List<string> userRoles = (List<string>)_userManager.GetRolesAsync(user).Result;

            List<RoleAssignViewModel> viewModel = new();

            foreach (var role in roles)
            {
                RoleAssignViewModel roleAssignViewModel = new();
                roleAssignViewModel.Id = role.Id;
                roleAssignViewModel.Name = role.Name;

                if (userRoles.Contains(role.Name))
                    roleAssignViewModel.Exist = true;

                else
                    roleAssignViewModel.Exist = false;

                viewModel.Add(roleAssignViewModel);

            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> roleAssignViewModels)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());

            foreach (var item in roleAssignViewModels)
            {
                if (item.Exist)

                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("Users");
        }

    }
}
