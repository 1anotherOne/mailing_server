using MimeKit;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;
using System;

namespace Server.Helpers
{
    public class EmailSenderHelper
    {
        private readonly Regex _regex;
        public EmailSenderHelper()
        {
            _regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        /// <summary>
        /// sends the mail using the outllok account credentials.
        /// </summary>
        /// <param name="destinationAddress"> the receiver email address.</param>
        public void SendEmail(string destinationAddress)
        {
            try
            {
                if (IsValidEmailAddress(destinationAddress))
                {
                    var messageContent = CreateMessage(destinationAddress);
                    using var client = new SmtpClient();
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("kybek29@outlook.com", "HelloWorld21!");
                    var options = FormatOptions.Default.Clone();
                    if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                        options.International = true;

                    client.Send(options, messageContent);

                    client.Disconnect(true);
                }
                else
                {
                    throw new ArgumentException("Your email address is not valid");
                }
            }catch(Exception err)
            {
                throw new ArgumentException("Your email address is not valid");
            }
        }
        /// <summary>
        /// creates the message.
        /// </summary>
        /// <param name="destinationAddress"> the destination address to who we want to send the mail.</param>
        /// <returns> the message built.</returns>
        //TODO: change the stub after the implementation of the model.
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
        /// <summary>
        /// validate the email address using the regex.
        /// </summary>
        /// <param name="destinationAddress"> the email address to who the email is sent.</param>
        /// <returns>true if the destination address is valide and false otherwise.</returns>
        private bool IsValidEmailAddress(string destinationAddress)
        {
            return _regex.IsMatch(destinationAddress);
        }
    }
}
