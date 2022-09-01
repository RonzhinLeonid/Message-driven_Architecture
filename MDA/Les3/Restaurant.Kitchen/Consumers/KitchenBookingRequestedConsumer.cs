using System.Threading.Tasks;
using MassTransit;
using Restaurant.Messages;

namespace Restaurant.Kitchen.Consumers
{
    internal class KitchenBookingRequestedConsumer : IConsumer<ITableBooked>
    {
        private readonly Manager _manager;

        public KitchenBookingRequestedConsumer(Manager manager)
        {
            _manager = manager;
        }

        public async Task Consume(ConsumeContext<ITableBooked> context)
        {
            Console.WriteLine($"[OrderId: {context.Message.OrderId} CreationDate: {context.Message.CreationDate}]");
            Console.WriteLine("Trying time: " + DateTime.Now);

            _manager.CheckKitchenReady(context.Message.OrderId, context.Message.PreOrder);

            await context.Publish<INotify>(new Notify(context.Message.OrderId, context.Message.ClientId, "Кухня дала добро"));
            await context.ConsumeCompleted;
        }
    }
}
