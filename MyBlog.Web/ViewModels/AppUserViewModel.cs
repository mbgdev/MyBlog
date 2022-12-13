using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.ViewModels
{
    
    
    public class AppUserViewModel
    {
        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        [DisplayName("E-Posta")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Ad")]
        public string  Name { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Soyad")]
        public string  Surname { get; set; }


        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [DisplayName("Doğum Günü")]
        public DateTime BirthDay { get; set; }
        

        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        [DisplayName("Telefon")]
        public string PhoneNumber { get; set; }


    }
}
