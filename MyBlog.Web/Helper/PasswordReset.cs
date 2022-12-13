using System.Net.Mail;
using System.Net;

namespace MyBlog.Web.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link, string email,string name)
        {
            MailMessage mailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");


            mailMessage.From = new MailAddress("proje1618@gmail.com");
            mailMessage.To.Add($"{email}");
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8; 
            mailMessage.Subject = "Şifre sıfırlama bağlantısı";
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding=System.Text.Encoding.UTF8;
            mailMessage.Body += $"Merhaba {name} <br/>";
            mailMessage.Body += "Şifrenizi yenilemek için lütfen butona tıklayanız<hr/><br/>";
            mailMessage.Body += $"<a  style=\"background-color: #f44336; color: white; padding: 15px 25px; text-align: center;  text-decoration: none; display: inline-block;\" href={link} target=\"_blank\" > Şifre Yenileme</a><br/><br/>";

            mailMessage.Body += "Eğer buton çalışmıyorsa aşağıdaki linki kullana bilirisiniz<br/><br/>";
            mailMessage.Body += link;


            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("proje1618@gmail.com", "ddfmobmenfhsrihk");
            smtpClient.Send(mailMessage);

        }
    }
}
