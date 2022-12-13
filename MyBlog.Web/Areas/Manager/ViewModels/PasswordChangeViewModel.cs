using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.Areas.Manager.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Eski Şifreniz")]
        public string PasswordOld { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Yeni Şifreniz")]
    
        public string PasswordNew { get; set; }
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Yeni Şifreniz Tekrar")]
        [Compare("PasswordNew", ErrorMessage ="Şifreler Eşlemiyor")]
        public string PasswordConfirm { get; set; }
    }
}
