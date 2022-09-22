using EmailSender.ContainerConsumers.Messages;

namespace EmailSender.Service
{
    /// <summary>
    /// Interface for Message Service
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        public void SendMessege(MessageToSend message);
    }
}
