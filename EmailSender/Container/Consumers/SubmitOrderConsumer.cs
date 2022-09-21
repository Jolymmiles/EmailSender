using EmailSender.ContainerConsumers.Messages;
using EmailSender.Service;
using MassTransit;

namespace EmailSender.Container.Consumers
{
    public class SubmitOrderConsumer : IConsumer<MessageToSend>
    {

        private readonly IMessageService _messageService;

        public SubmitOrderConsumer(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Consume(ConsumeContext<MessageToSend> context)
        {
            _messageService.SendMessege(context.Message);
        }
    }
}
