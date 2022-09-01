﻿namespace Restaurant.Messages
{
    public interface ITableBooked
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public DateTime CreationDate { get; }
        public Dish? PreOrder { get; }
    }

    public class TableBooked : ITableBooked
    {
        public TableBooked(Guid orderId, Guid clientId, Dish? preOrder, DateTime creationDate)
        {
            OrderId = orderId;
            ClientId = clientId;
            PreOrder = preOrder;
            CreationDate = creationDate;
        }

        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public DateTime CreationDate { get; }
        public Dish? PreOrder { get; }
    }
}