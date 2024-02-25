using System;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using FluentValidator;

namespace BaltaStore.Domain.StoreContext.Entites
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.WAITING;
        }
        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            //se a data estimada de entrega for no passado, não entregar
            Status = EDeliveryStatus.SHIPPED;
        }

        public void Cancel()
        {
            // se o status já estiver entregue, não pode cancelar
            Status = EDeliveryStatus.CANCELLED;
        }
    }
}