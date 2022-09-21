namespace EmailSender.ContainerConsumers.Messages
{
    /// <summary>
    /// Message
    /// </summary>
    public class MessageToSend
    {
        /// <summary>
        /// Message constuctor
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <param name="textOfEmail"></param>
        /// <param name="subject"></param>
        public MessageToSend(string emailAdress, string textOfEmail, string subject)
        {
            EmailAdress = emailAdress;
            TextOfEmail = textOfEmail;
            Subject = subject;
        }

        /// <summary>
        /// Mail adress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string TextOfEmail { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }



    }
}