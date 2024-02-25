using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Tests;

namespace BaltaStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void ShouldRegisterCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Andre";
            command.LastName = "Baltieri";
            command.Document = "28659170377";
            command.Email = "andrebaltieri@gmail.com";
            command.Phone = "11999999997";


            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handler(command);
            Assert.AreEqual(true, handler.IsValid);
            Assert.AreNotEqual(null, result);

        }
    }
}