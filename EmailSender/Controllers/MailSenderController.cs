using EmailSender.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class MailSenderController : ControllerBase
    {

        private readonly ILogger<MailSenderController> _logger;

        private readonly IMessageService _messageService;

        public MailSenderController(ILogger<MailSenderController> logger, IMessageService messageService)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Get test mail
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetTestMail")]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Succes get");
                var message = new Message("some@gmail.com", "someText", "some sub");
                return Ok(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return BadRequest();
            }

        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost(Name = "SendMail")]
        public IActionResult SendMessage(Message data)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            try
            {
                if (!Regex.IsMatch(data.EmailAdress, pattern, RegexOptions.IgnoreCase))
                    throw new ArgumentException("NonValid email", data.EmailAdress);
                _messageService.SendMessege(data);
                _logger.LogInformation("Succes sending");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return BadRequest("Non valid email");
            }
        }
    }
}