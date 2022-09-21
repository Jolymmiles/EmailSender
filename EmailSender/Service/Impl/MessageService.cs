using EmailSender.ContainerConsumers.Messages;
using EmailSender.Properties;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Text.RegularExpressions;

namespace EmailSender.Service.Impl
{
    /// <summary>
    /// Message Service
    /// </summary>
    public class MessageService : IMessageService
    {

        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        public async void SendMessege(MessageToSend message)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (!Regex.IsMatch(message.EmailAdress, pattern, RegexOptions.IgnoreCase) | message.Subject == null | message.TextOfEmail == null)
                throw new ArgumentException();

            var clientSecrets = new ClientSecrets
            {
                ClientId = Resources.ClientId,
                ClientSecret = Resources.ClientSecret,
            };


            var googleCredentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, new[]
            { GmailService.Scope.MailGoogleCom }, "geography.pet.project.mail.sender@gmail.com", CancellationToken.None
            );
            if (googleCredentials.Token.IsExpired(SystemClock.Default))
            {
                await googleCredentials.RefreshTokenAsync(CancellationToken.None);
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                var oauth2 = new SaslMechanismOAuth2(googleCredentials.UserId, googleCredentials.Token.AccessToken);
                client.Authenticate(oauth2);

                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("geography.pet.project.mail.sender@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(message.EmailAdress));
                mail.Subject = message.Subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = message.TextOfEmail };

                try
                {
                    await client.SendAsync(mail);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.GetBaseException().Message);
                }

                client.Disconnect(true);
            }

        }

    }
}
