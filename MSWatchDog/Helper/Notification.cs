using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;

using NLog;

namespace MSWatchDog.Helper
{
    public static class Notification
    {
        public static void Notify(NotificationType type, string Message)
        {
            switch (type)
            {
                case NotificationType.Email:
                    {
                        SendEmail(Message);
                        break;
                    }
                case NotificationType.SMS:
                    {
                        SendSms(Message);
                        break;
                    }
            }
        }

        private static void SendEmail(string Message)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(Config.FromEmailAlert);
            message.Subject = "MSWatchDog alert";
            message.Body = Message;

            foreach (var email in Config.ToEmailsAlert)
            {
                message.To.Add(email);
            }

            SmtpClient client = new SmtpClient(Config.SMTPServer);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Config.SMTPUserName, Config.SMTPPassword);
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Logger.WriteError("SendEmail error", ex);
            }
        }

        private static void SendSms(string Message)
        {
            throw new NotImplementedException();
        }

    }

    public enum NotificationType
    {
        Email,
        SMS
    }
}