using System;
using System.Net.Mail;

namespace VL.ItsMe1110.Custom.Emails
{
    public class MailHelper
    {
        public void SendMail()
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("to1@xxx.com");
            msg.To.Add("to2@xxx.com");
            msg.CC.Add("cc1@xxx.com");
            msg.CC.Add("cc2@xxx.com");
            msg.Bcc.Add("bcc1@xxx.com");
            msg.Bcc.Add("bcc2@xxx.com");
            msg.From = new MailAddress("sender@xxx.com", "发送人", System.Text.Encoding.UTF8);
            msg.Subject = "这是测试邮件";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "邮件内容";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("account", "password");
            client.Host = "smtp.xxx.com";
            object userState = msg;
            try
            {
                client.SendAsync(msg, userState);
                //发送成功
            }
            catch (Exception ex)
            {
                //发送邮件出错
            }
        }
    }
}