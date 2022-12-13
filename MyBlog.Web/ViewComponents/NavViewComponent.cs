using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.ViewComponents
{
    public class NavViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
