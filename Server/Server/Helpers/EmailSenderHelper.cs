using MimeKit;
using MailKit.Net.Smtp;

namespace Server.Helpers
{
    public class EmailSenderHelper
    {
        public void SendEmail(string destinationAddress)
        {
            var messageContent = CreateMessage(destinationAddress);
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("kybek29@outlook.com", "HelloWorld21!");
                var options = FormatOptions.Default.Clone();
                if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                    options.International = true;

                client.Send(options, messageContent);

                client.Disconnect(true);
            }
        }
        private MimeMessage CreateMessage(string destinationAddress)
        {
            var email = new MimeMessage();
            var fromAdress = new MailboxAddress("mailing news", "kybek29@outlook.com");
            var toAdress = new MailboxAddress(string.Empty,destinationAddress);
            email.From.Add(fromAdress);
            email.To.Add(toAdress);
            email.Subject = "2020 is over!";
            email.Body = new TextPart("plain")
            {
                Text = "happy new year"
            };
            return email;
        }
    }
}
