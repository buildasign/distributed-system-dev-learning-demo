using System;
using System.Threading.Tasks;
using eCommDemo.Messages;
using MassTransit;

namespace eCommDemo.Message.Listener
{
    internal class Program
    {
        private static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "mfg_queue", c => { c.Consumer<CreateOrderHandler>(); });
            });

            var busHandle = bus.Start();
        }
    }

    public class CreateOrderHandler : IConsumer<CreateOrder>
    {
        public Task Consume(ConsumeContext<CreateOrder> context)
        {
            Console.WriteLine("creating a new product(s)");
            return Task.FromResult(0);
        }
    }
}