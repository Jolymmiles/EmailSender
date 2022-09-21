namespace EmailSender
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
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

        /// <summary>
        /// Message constuctor
        /// </summary>
        /// <param name="emailAdress"></param>
        /// <param name="textOfEmail"></param>
        /// <param name="subject"></param>
        public Message(string emailAdress, string textOfEmail, string subject)
        {

            this.EmailAdress = emailAdress;
            this.TextOfEmail = textOfEmail;
            this.Subject = subject;
        }
    }
}