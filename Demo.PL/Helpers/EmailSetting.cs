using Demo.DAL.Models;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helpers
{
    public static class EmailSetting
    {
        public static void SendMail(Email mail)
        {
            var mailServer = new SmtpClient("smtp.gmail.com", 587);
            mailServer.EnableSsl = true;
            mailServer.Credentials = new NetworkCredential("amlmohammed60@gmail.com", "nvwgmoerkgjjjvps");
           // xrvd kbeu egkf lorc
            mailServer.Send("amlmohammed60@gmail.com",mail.To,mail.Subject,mail.Body);
        }
    }
}
