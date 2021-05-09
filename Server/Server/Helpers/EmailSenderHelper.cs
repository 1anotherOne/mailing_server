using MimeKit;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using NewsAPI.Models;

namespace Server.Helpers
{
    public class EmailSenderHelper
    {
        private readonly Regex _regex;
        private const string emailAddress = "kybek29@outlook.com";

        public EmailSenderHelper()
        {
            _regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        /// <summary>
        /// sends the mail using the outllok account credentials.
        /// </summary>
        /// <param name="destinationAddress"> the receiver email address.</param>
        public void SendEmail(string query, string destinationAddress, List<Article> articles)
        {
            try
            {
                if (IsValidEmailAddress(destinationAddress))
                {
                    var messageContent = CreateMessage(query, articles, destinationAddress);
                    using var client = new SmtpClient();
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(emailAddress, "HelloWorld21!");
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
                throw new ArgumentException(err.Message);
            }
        }

        /// <summary>
        /// sends a mail for tests using the outllok account credentials.
        /// </summary>
        /// <param name="destinationAddress"> the receiver email address.</param>
        public void SendEmailTest(string destinationAddress)
        {
            try
            {
                if (IsValidEmailAddress(destinationAddress))
                {
                    var messageContent = CreateMessageTest(destinationAddress);
                    using var client = new SmtpClient();
                    client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(emailAddress, "HelloWorld21!");
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
            }
            catch (Exception err)
            {
                throw new ArgumentException(err.Message);
            }
        }

        /// <summary>
        /// creates the test message.
        /// </summary>
        /// <param name="destinationAddress"> the destination address to who we want to send the mail.</param>
        /// <returns> the message built.</returns>
        //TODO: change the stub after the implementation of the model.
        private MimeMessage CreateMessageTest(string destinationAddress)
        {
            var email = new MimeMessage();
            var fromAddress = new MailboxAddress("mailing news", emailAddress);
            var toAddress = new MailboxAddress(string.Empty,destinationAddress);
            email.From.Add(fromAddress);
            email.To.Add(toAddress);
            email.Subject = "2020 is over!";
            email.Body = new TextPart("plain")
            {
                Text = "happy new year"
            };
            return email;
        }

        /// <summary>
        ///  creates the message containing the list of articles
        /// </summary>
        /// <param name="query">The query to be displayed as the email's subject</param>
        /// <param name="articles"> The list of articles received from the NewsAPI</param>
        /// <param name="destinationAddress"> the destination address</param>
        /// <returns></returns>
        private MimeMessage CreateMessage(string query,List<Article> articles , string destinationAddress)
        {
            var email = new MimeMessage();
            var fromAddress = new MailboxAddress("mailing news", emailAddress);
            var toAddress = new MailboxAddress(string.Empty, destinationAddress);
            email.From.Add(fromAddress);
            email.To.Add(toAddress);
            email.Subject = query;
            string emailText = "";
            int number = 1;
            foreach (Article article in articles)
            {
                emailText += "News number: " + number + "\n";
                emailText += ArticleToString(article);
                number += 1;
                emailText += "\n";
            }
            email.Body = new TextPart("plain")
            {
                Text = emailText
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

        private string ArticleToString(Article article)
        {
            string ret = "";
            ret += "Author: " + article.Author + "\n";
            ret += "Title: " + article.Title + "\n";
            ret += "Description: " + article.Description + "\n";
            ret += "Source: " + article.Source + "\n";
            ret += "URL: " + article.Url + "\n";
            ret += "Publish date: " + article.PublishedAt + "\n";
            return ret;
        }
    }
}
