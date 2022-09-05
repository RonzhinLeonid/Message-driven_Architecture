﻿namespace Restaurant.Messages
{
    public interface IKitchenReady
    {
        public Guid OrderId { get; }
    }

    public class KitchenReady : IKitchenReady
    {
        public KitchenReady(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}
