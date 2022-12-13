
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MyBlog.Web.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [EmailAddress(ErrorMessage ="E-Posta formatı yanlıştır.")]
        [DisplayName("E-Posta")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Şifre")]
        public string Password { get; set; }
    }
}
