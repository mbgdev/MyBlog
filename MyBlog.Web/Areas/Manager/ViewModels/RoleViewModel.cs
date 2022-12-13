using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Areas.Manager.ViewModels
{
    public class RoleViewModel
    {

        [Required(ErrorMessage = "Bu alan Boş Geçilemez")]
        [DisplayName("Rol İsmi")]
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
