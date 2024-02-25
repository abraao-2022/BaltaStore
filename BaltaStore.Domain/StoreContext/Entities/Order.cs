using System;
using System.Collections.Generic;
using System.Linq;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Entities;
using FluentValidator;

namespace BaltaStore.Domain.StoreContext.Entites
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.CREATED;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

        public void AddItem(Product product, decimal quantity)
        {
            if (quantity > product.QuantityOnHand)
                AddNotification("OrderItem", $"Produto {product.Title} não tem {quantity} itens em estoque.");

            var item = new OrderItem(product, quantity);
            _items.Add(item);
        }


        // criar um pedido
        public void Place()
        {
            // gera o número do pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            if (_items.Count == 0)
                AddNotification("Order", "Este pedido não possui itens");
            //validar
        }

        // pagar um pedido
        public void Pay()
        {
            Status = EOrderStatus.PAID;
        }
        // enviar um pedido
        public void Ship()
        {
            // a cada 5 produtos é uma entrega
            var deliveries = new List<Delivery>();
            var count = 0;

            // quebra as entregas
            foreach (var item in _items)
            {
                count++;
                if (count == 5)
                {
                    count = 0;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
            }

            // envia todas as entregas
            deliveries.ForEach(x => x.Ship());

            //adiciona as entregas ao pedido
            deliveries.ForEach(x => _deliveries.Add(x));

        }

        // cancelar um pedido
        public void Cancel()
        {
            Status = EOrderStatus.CANCELED;
            _deliveries.ToList().ForEach(x => x.Cancel());
        }

    }
}