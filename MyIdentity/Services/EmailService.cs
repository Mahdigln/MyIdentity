using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyIdentity.Services;

    public  class EmailService
    {
        // این کد خود آموزش بود کار نمیکرد
        //public Task Execute(string UserEmail, string Body, string Subject)
        //{
        //    //enable less secure apps in account google with link
        //    //https://myaccount.google.com/lesssecureapps

        //    SmtpClient client = new SmtpClient();
        //    client.Port = 587;
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true;
        //    client.Timeout = 1000000;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    //در خط بعدی ایمیل  خود و پسورد ایمیل خود  را جایگزین کنید
        //    client.Credentials = new NetworkCredential("identityiden067@gmail.com", "rjqdpujujlkbilfp");
        //    MailMessage message = new MailMessage("tt9407312@gmail.com", UserEmail, Subject, Body);
        //    message.IsBodyHtml = true;
        //    message.BodyEncoding = UTF8Encoding.UTF8;
        //    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        //    client.Send(message);
        //    return Task.CompletedTask;
        //}

        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("identityiden067@gmail.com", "Identity");//اینجا باید ایمیلمون رو بنویسیم
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

           
            SmtpServer.Port = 587;
            // SmtpServer.UseDefaultCredentials = false;  //stack over flow
            SmtpServer.Credentials = new System.Net.NetworkCredential("identityiden067@gmail.com", "rjqdpujujlkbilfp");//اینجا باید ایمیل و پسوردش رو بنویسیم
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }

