using EmailSender.Properties;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

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
        /// SendMessage
        /// </summary>
        /// <param name="message"></param>
        public async void SendMessege(Message message)
        {
            try
            {

                var clientSecrets = new ClientSecrets
                {
                    ClientId = Resources.ClientId,
                    ClientSecret = Resources.ClientSecret,
                };

                var googleCredentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, new[]
                {
                        GmailService.Scope.MailGoogleCom }, "geography.pet.project.mail.sender@gmail.com", CancellationToken.None
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
                    await client.SendAsync(mail);
                    client.Disconnect(true);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }

    }
}
