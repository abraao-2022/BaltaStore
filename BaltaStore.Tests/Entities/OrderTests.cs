using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Domain.StoreContext.ValueObjects;

namespace BaltaStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Product _mouse;
        private Product _keyboard;
        private Product _chair;
        private Product _monitor;
        private Customer _customer;
        private Order _order;

        public OrderTests()
        {
            var name = new Name("Andre", "Baltieti");
            var document = new Document("46718115533");
            var email = new Email("hello@balta.io");
            var _customer = new Customer(name, document, email, "551999876542");
            _order = new Order(_customer);
            _mouse = new Product("Mouse gamer", "Mouse gamer", "mouse.jpg", 100M, 10);
            _keyboard = new Product("Teclado gamer", "Teclado gamer", "teclado.jpg", 100M, 10);
            _chair = new Product("Cadeira gamer", "Cadeira gamer", "cadeira.jpg", 100M, 10);
            _monitor = new Product("Monitor gamer", "Monitor gamer", "monitor.jpg", 100M, 10);
        }

        // Consigo criar um novo pedido
        [TestMethod]
        public void ShouldCreateOrderWhenValid()
        {
            Assert.AreEqual(true, _order.IsValid);
        }

        // Ao criar o pedido, o status deve ser created
        [TestMethod]
        public void StatusShouldBeCreatedWhenOrderCreated()
        {
            Assert.AreEqual(EOrderStatus.CREATED, _order.Status);
        }

        // ao adicionar um novo item, a quantidade de itens deve mudar
        [TestMethod]
        public void ShouldReturnTwoWhenAddedTwoValidItems()
        {
            _order.AddItem(_monitor, 5);
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(2, _order.Items.Count);
        }

        // ao adicionar um novo item, deve subtrair a quantidade do produto
        [TestMethod]
        public void ShouldReturnFiveWhenAddedPurchasedFiveItem()
        {
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        // ao confirmar pedido, deve gerar um número
        [TestMethod]
        public void ShouldReturnANumberWhenOrderPlaced()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        }

        // ao pagar um pedido, o status deve ser paid
        [TestMethod]
        public void ShouldReturnPaidWhenOrderPaid()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.PAID, _order.Status);
        }

        // dados mais 10 produtos, devem haver duas entregas
        [TestMethod]
        public void ShouldTwoShippingsWhenPurchasedTenProducts()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);

            _order.Ship();
            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        // ao cancelar o pedido, o status deve ser cancelado
        [TestMethod]
        public void StatusShouldBeCanceledWhenOrderCanceled()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.CANCELED, _order.Status);
        }

        // ao cancelar o pedido, deve cancelar as entragas
        [TestMethod]
        public void ShouldCancelShippingWhenOrderCanceled()
        {
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.AddItem(_mouse, 1);
            _order.Ship();

            _order.Cancel();
            foreach (var x in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.CANCELLED, x.Status);
            }
        }

        public void CreateCustomer()
        {
            // verificar se o CPF ja existe
            // verificar se o E-mail ja existe
            // criar os VOs
            // criar a entidade
            // validar as entidades e VO
            // inserir o cliente no banco
            // envia convite do slack
            // envia o e-mail de boas vindas
        }
    }
}