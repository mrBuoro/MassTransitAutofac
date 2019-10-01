using MassTransit;
using Messages;
using System;
using System.Threading.Tasks;

namespace Server
{
    public class CheckOrderStatusConsumer :
        IConsumer<CheckOrderStatus>
    {
        public CheckOrderStatusConsumer()
        {
        }

        public async Task Consume(ConsumeContext<CheckOrderStatus> context)
        {
            CheckOrderStatus cos = context.Message;

            if (cos.OrderId == "error")
                throw new InvalidOperationException("Error... BRUM");

            //
            await context.RespondAsync<OrderStatusResult>(new
            {
                cos.OrderId,
                Timestamp = DateTime.Now,
                StatusCode = (short) 1,
                StatusText = "AAA"
            });
        }
    }
}
