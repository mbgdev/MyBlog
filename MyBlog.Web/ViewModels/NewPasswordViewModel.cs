﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Web.ViewModels
{
    public class NewPasswordViewModel
    {
        [Required(ErrorMessage ="Bu Alan Boş Geçilemez")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
       
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez")]
        [DisplayName("Şifre Tekrar")]
        [DataType(DataType.Password)]
       
        [Compare("Password",ErrorMessage ="Şifreler Eşleşmiyor")]
        public string PasswordConfirm { get; set; }


        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
