using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        [EmailAddress]
        [DisplayName("E-Posta")]
        public string Email { get; set; }



    }
}
